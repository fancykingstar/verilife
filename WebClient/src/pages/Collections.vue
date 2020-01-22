<template>
  <q-page class="q-pa-sm">
    <div class="i-coll-container">
          <div name="collections" v-show="tab === 'collections'">
            <div class="i-coll-title"> COLLECTIONS </div>
            <q-scroll-area style="height: calc(100vh - 150px);"  :thumb-style="thumbStyle"
              :content-style="contentStyle"
              :content-active-style="contentActiveStyle">
                <div class="i-group-list">
                    <div @click="groupClicked(group)"
                    v-bind:class="{'i-group-list-container': true, 'i-group-favorite': group.favorite === true}"
                    v-for="(group, idx) in groups" :key="idx">
                      <div class="i-group-heart" @click.stop ="groupFavoriteClicked(group.code)">
                          <q-icon v-if="group.favorite === true" name="favorite"/>
                          <q-icon v-if="group.favorite === false" name="favorite_border"/>
                        </div>
                      <div class="i-group-list-container-img">
                        <span v-if="group.images.length === 0"> {{group.webDescription}} </span>
                        <q-img v-if="group.images.length === 1"
                          :src="getImageUrl(group.images[0].id)"
                          spinner-color="grey-5"
                          style="width: 80%"
                        >
                       <!-- <div v-if="group.favorite === false" class="absolute-full text-subtitle2 flex flex-center">
                        </div> -->
                       </q-img>
                    </div>
                  </div>
                </div>
               </q-scroll-area>
          </div>
          <div name="watches" v-if="tab === 'watches'">
            <div class="i-coll-title"> {{selectedGroupName}} </div>
            <q-scroll-area  ref="scrollArea" style="height: calc(100vh - 150px);"  :thumb-style="thumbStyle"
              :content-style="contentStyle"
              :content-active-style="contentActiveStyle">
                <watchlist :list="watchList" ref="wl" @loadNext="loadMoreItems"/>
             </q-scroll-area>
        </div>
    </div>
     <q-page-sticky position="top-left" :offset="[20, 18]" v-show="tab === 'watches'" >
        <q-btn round color="grey-6" icon="arrow_back" @click="tab = 'collections'"/>
    </q-page-sticky>

  </q-page>
</template>
<style>
.i-group-favorite{
   border: 8px solid #FFF200!important;
   opacity: 1!important;
}
.i-group-favorite .i-group-heart{
  color: #FFF200;
}
.i-group-heart{
  color: #58595B;
  position: absolute;
  top: 0px;
  right: 7px;
  font-size: 20px;
}
.i-coll-title{
  text-align: center;
  padding: 10px;
  font-size: 20px;
  color:#58595B;
  font-family: Din;
  font-weight: bold;
}
.i-coll-container{
  padding-left:10%;
  padding-right:10%;
}
.i-group-list {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: center;
}
.i-group-list-container {
  width:240px;
  height:220px;
  border: 8px solid #D5D6D7;
  display:flex;
  flex-direction: column;
  margin: 10px;
  padding: 40px;
  cursor: pointer;
  background: black;
  justify-content: center;
  position: relative;
  overflow: hidden;
  opacity: 0.8;
}
.i-group-list-container-img{
  color:white;
  font-size:30px;
  text-align: center
}

/* 600px or less */
@media only screen and (max-width: 600px) {
  .i-coll-container {
    padding-left: 2%;
    padding-right: 2%;
}
.i-group-list-container{
  min-width: 90%;
}
.i-watch-list-container {
    min-width: 90%;

}
}
</style>

<script>
// import VueJsonPretty from 'vue-json-pretty'
import _ from 'lodash'
import contactService from '../app/services/contactservice'
import watchlist from '../components/watchlist'
import userData from '../app/services/userdata'

export default {
  name: 'Collections',
  components: {
    watchlist: watchlist
    // VueJsonPretty
  },
  data () {
    return {
      tab: 'collections',
      splitterModel: 20,
      contact: null,
      json: '',
      groups: [],
      offerDialog: false,
      selectedOffer: {
        description: '',
        items: []
      },
      watchList: [],
      selectedGroupName: '',
      currentItemPage: 1,
      selectedGroupCode: 0
    }
  },
  computed: {
    contentStyle () {
      return {
      }
    },
    contentActiveStyle () {
      return {
      }
    },
    thumbStyle () {
      return {
        right: '2px',
        borderRadius: '5px',
        backgroundColor: '#58595B',
        width: '5px',
        opacity: 0.75
      }
    }
  },
  created: function () {
    var me = this
    var contact = userData.getUserData()
    if (contact) {
      me.contact = contact
    } else {
      return
    }
    me.$q.loading.show()
    contactService.getPublishedItems().then((data) => {
      me.json = data
      me.groups = _.reverse(_.sortBy(data.groups, g => { return g.images.length })) // _.filter(data.groups, g => { return g.images.length === 1 })
      contactService.contactGetAttributes(me.contact.id).then((attrList) => {
        var favoriteGroup = _.find(attrList, { name: 'Favorite Watch Collection' })
        if (favoriteGroup) {
          var group = _.find(me.groups, { code: favoriteGroup.value })
          group.favorite = true
        }
      })
    }, (error) => {
      console.error(error.message)
    }).then(() => {
      me.$q.loading.hide()
    })
  },
  methods: {
    groupFavoriteClicked (code) {
      var me = this
      _.each(me.groups, (g) => {
        g.favorite = false
      })
      var group = _.find(me.groups, { code: code })
      group.favorite = true
      me.setFavoriteGroup(group)
      console.log(group)
    },
    loadMoreItems (done) {
      var me = this
      if (me.currentItemPage === -1) {
        return
      }
      me.currentItemPage = me.currentItemPage + 1
      me.$q.loading.show()
      contactService.getItemsPerGroup(me.selectedGroupCode, me.currentItemPage).then((data) => {
        if (data.items.length === 0) {
          me.currentItemPage = -1
        }
        _.each(data.items, i => {
          me.watchList.push(i)
        })
        if (done) {
          done()
        }
      }, (error) => {
        console.error(error.message)
      }).then(() => {
        me.$q.loading.hide()
      })
    },
    groupClicked (group) {
      var me = this
      me.selectedGroupName = group.webDescription
      me.selectedGroupCode = group.code
      me.currentItemPage = 0
      me.watchList = []
      me.loadMoreItems(() => {
        me.tab = 'watches'
      })
    },
    setFavoriteGroup (group) {
      var me = this
      var data = {
        account: me.contact.id,
        clubCode: '100',
        attributes: [
          {
            name: 'Favorite Watch Collection',
            value: group.code
          }
        ]
      }
      contactService.contactSetAttributes(JSON.stringify(data)).then(() => {
        me.$q.notify({
          color: 'grey-6',
          position: 'bottom',
          message: 'Favorite collection selected',
          icon: 'done'
        })
      }, (error) => {
        console.error(error.message)
      })
    },
    getImageUrl (id) {
      return process.env.API + '/images/' + id
    }
  }
}
</script>
