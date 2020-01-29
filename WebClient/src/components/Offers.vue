<template>
 <!-- <q-page class="q-pa-sm"> -->
   <div class="i-offer">
    <div class="i-offer-container">
      <div class="i-offer-item"  v-for="(offer, idx) in offers" :key="idx"  @click="openOffer(offer)">
        <q-img
        class="i-offer-img2"
          :src="getImageUrl(offer.images[0].id)"
          :ratio="3/1"
          style="max-height:350px"
          spinner-color="grey-5"
          />
        <!-- <img class="i-offer-img" :src="getImageUrl(offer.images[0].id)"> -->
      </div>
    </div>
    <q-dialog v-model="offerDialog" :maximized="maximizedToggle"
      transition-show="slide-up"
      transition-hide="slide-down">
      <div class="i-off-container" >
       <div class="i-off-close">
       <q-btn icon="close" flat round dense v-close-popup />
       </div>
        <div class="i-off-body" v-if="selectedOffer.description || selectedOffer.details ">
         <div class="text-h6">{{selectedOffer.description}}</div>
         <div class="text-h6">{{selectedOffer.details}}</div>
        </div>
        <div v-html="html"></div>
        </div>
    </q-dialog>
    </div>
 <!-- </q-page> -->
</template>

<style>
.i-offer{
  display: flex;
  width:100%;
}
.i-offer-title{
    text-align: center;
  padding: 10px;
  font-size: 20px;
   color:#58595B;
        font-family: Din;
        font-weight: bold;
}
.i-offer-item{
display: flex;
flex-direction: row;
justify-content: center;
padding:10px 0px;
cursor: pointer;
}
.i-offer-img{
  max-width: 100%;
    max-height: 200px;
}
.i-offer-container{
  display: flex;
  flex-direction: column;
  justify-content: center;
      width: 100%;
}
.i-off-container{
  display: flex;
  flex-direction: column;
  border:10px solid #333333;
  background: white;
  max-width: 800px!important;
}
.i-off-close{
  display:flex;
  flex-direction: row;
  justify-content: flex-end;
  padding:3px;
}
.i-off-body{
  padding:20px;
}
@media only screen and (max-width: 600px) {
.i-off-container{
   max-width: 100%!important;
}

}

</style>

<script>
// import VueJsonPretty from 'vue-json-pretty'

import contactService from '../app/services/contactservice'
import userData from '../app/services/userdata'

export default {
  name: 'Offers',
  components: {
    // VueJsonPretty
  },
  data () {
    return {
      contact: null,
      json: '',
      html: '',
      offers: [],
      offerDialog: false,
      selectedOffer: {
        description: '',
        items: []
      },
      maximizedToggle: this.$q.platform.is.mobile
    }
  },
  created: function () {
    var me = this
    me.iunitalize()
  },
  methods: {
    iunitalize () {
      var me = this
      var contact = userData.getUserData()
      if (contact) {
        me.contact = contact
      } else {
        me.$router.push('/')
      }
      console.log(me.contact)
      contactService.publishedOffersGet(me.contact.account.id, me.contact.externalInfo.companyName).then((offers) => {
        me.json = offers
        me.offers = offers
      }, (error) => {
        console.error(error.message)
      })
    },
    refresh () {
      var me = this
      me.iunitalize()
    },
    getImageUrl (id) {
      return process.env.API + '/images/' + id
    },
    openOffer (offer) {
      var me = this
      console.log(offer)
      me.$q.loading.show()
      me.selectedOffer = offer
      contactService.getOfferHtml(offer.id).then((html) => {
        me.offerDialog = true
        me.html = html
      }, (error) => {
        console.error(error.message)
      }).then(() => {
        me.$q.loading.hide()
      })
    }
  }
}
</script>
