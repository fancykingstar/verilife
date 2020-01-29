<template>
  <div class="i-not-container">
    <div class="i-not-title">NOTIFICATIONS</div>
      <div class="i-not-item" v-for="(noti, idx) in notifications" :key="idx">
         <div v-if="noti.claimAttributeName" style="width:30%">
            <div v-if="noti.attrValue === 'Yes'" class="i-claimed">Claimed </div>
            <div class="i-btn-claim" v-if="noti.attrValue !== 'Yes'" @click="setAttribute(noti.claimAttributeName,'Yes')">Claim</div>
            <!-- <div class="i-btn-claim"  @click="setAttribute(noti.claimAttributeName,'No')">unClaim</div> -->
          </div>
        <div class="i-not-img">
          <q-img
              :src="getImageUrl(noti.pictureId)"
              spinner-color="grey-5"
            />
          <!-- <img class="t-h-img" :src="getItemImageUrl(sale.item.id)"/> -->
        </div>
        <div class="i-not-body">
          <div class="i-not-body-date">{{ noti.date }}</div>
          <div class="i-not-body-name">  {{ noti.description }}</div>
          <div class="i-not-body-price">{{noti.details}} </div>

        </div>
      </div>
      <!-- <q-btn lable="click" @click="setCaseAttributeGroup()"/> -->

      <q-dialog v-model="confirmAddress">
      <q-card>
        <q-card-section class="row items-center">
          <span class="q-ml-sm">Your shipping address is missing.</span>
        </q-card-section>

        <q-card-actions align="right">
          <q-btn flat label="Cancel" color="grey" v-close-popup />
          <q-btn flat label="Fill out" color="grey" @click="gotoAddress" />
        </q-card-actions>
      </q-card>
    </q-dialog>
</div>
</template>

<style>
.i-claimed{
   display: flex;
  padding: 5px;
  text-transform: uppercase;
  font-size: 20px;
  line-height: 30px;
  color: #58595B;
  font-weight: bold;
  height: 40px;
  cursor: pointer;
  font-family: Din;
  margin-top: 20px;
}
.i-btn-unclaim {
  display: flex;
  justify-content: center;
  padding: 5px;
  background-color: #C7C8CA;
  text-transform: uppercase;
  font-size: 20px;
  line-height: 30px;
  color: #58595B;
  font-weight: bold;
  height: 40px;
  cursor: pointer;
  font-family: Din;
  margin-top: 20px;
}
.i-btn-claim {
  display: flex;
  justify-content: center;
  padding: 5px;
  background-color: #FFF200;
  text-transform: uppercase;
  font-size: 20px;
  line-height: 30px;
  color: #58595B;
  font-weight: bold;
  height: 40px;
  cursor: pointer;
  font-family: Din;
  margin-top: 20px;
}
.i-not-container{
  display: flex;
  flex-direction: column;
  padding:10px;
  padding-left:10%;
  padding-right:10%;
}

.i-not-title{
  text-align: center;
  padding: 10px;
  font-size: 20px;
  color:#58595B;
  font-family: Din;
  font-weight: bold;
}
.i-not-item{
  padding: 10px;
  display: flex;
  flex-direction: column-reverse;
  padding-bottom:20px;
  border-bottom:1px solid #cacbce;
}
.i-not-img{
  width:30%;
  justify-content: center;
    width: 100%;
    display: flex;
}
.i-not-body{
  display:flex;
  flex-direction: column;
  padding-bottom:10px;
}
.i-not-body-date{
  color:#7d7d7d;
}
.i-not-body-name{
  color:#58595B;
  font-family: Din;
  font-weight: bold;
  font-size: 18px;
}
.i-not-body-price{
   color:#7d7d7d;
}
@media only screen and (max-width: 400px) {
  .i-not-container{
    padding-left:5%;
    padding-right:5%;
  }
}
</style>

<script>
// import VueJsonPretty from 'vue-json-pretty'

import contactService from '../app/services/contactservice'
import _ from 'lodash'
import moment from 'moment'
import EventBus from '../app/globalbus'
import userData from '../app/services/userdata'

export default {
  name: 'Notifications',
  components: {
    // VueJsonPretty
  },
  data () {
    return {
      confirmAddress: false,
      contact: null,
      json: '',
      notifications: []
    }
  },
  created: function () {
    var me = this
    me.initalize()
  },
  methods: {
    gotoAddress () {
      var me = this
      me.$router.push('/dashboard/contact/address')
    },
    initalize () {
      var me = this
      me.contact = userData.getUserData()
      if (me.contact == null) { return }
      // todo: combine in one requrest in nav
      contactService.notificationsGetByContactId(me.contact.account.id).then((notifications) => {
        var readedNotinfications = []
        _.each(notifications, n => {
          readedNotinfications.push(n.code)
        })
        localStorage.setItem('READ_NOTILIST', JSON.stringify(readedNotinfications))
        EventBus.$emit('NOT_UPDATED', 0)
        contactService.contactGetAttributes(me.contact.id).then((attrList) => {
          me.json = notifications
          _.each(notifications, n => {
            n.date = moment(n.createdDate).format('MMMM Do YYYY')
            n.attrValue = ''
          })
          _.each(attrList, (attribute) => {
            var noti = _.find(notifications, { claimAttributeName: attribute.name })
            if (noti) {
              noti.attrValue = attribute.value
            }
          })
          me.notifications = notifications
        })
      }, (error) => {
        console.error(error.message)
      })
    },
    checkMemberAttributes (notifications) {
      var me = this
      var not = _.find(notifications, { description: 'Get a free-limited edition 50 slot case' })
      if (not) {
        not.button = ''
      }
      contactService.contactGetAttributes(me.contact.id).then((attrList) => {
        _.each(attrList, (attribute) => {
          var noti = _.find(notifications, { claimAttributeName: attribute.name })
          if (noti) {
            noti.attrValue = attribute.value
          }
        })
      })
    },
    getImageUrl (id) {
      return process.env.API + '/images/' + id
    },
    checkAddress () {
      var me = this
      me.contact = userData.getUserData()
      if (me.contact.addresses.length > 0) {
        var address = me.contact.addresses[0]
        if (address.address1.length > 0 && address.city.length > 0 && address.postCode.length > 0 && address.stateProvinceRegion.length > 0 && address.country.length > 0) {
          return true
        }
      }
      return false
    },
    setAttribute (name, value) {
      var me = this
      if (me.checkAddress() === false) {
        me.confirmAddress = true
        return
      }
      var data = {
        account: me.contact.id,
        clubCode: '100',
        attributes: [
          {
            name: name,
            value: value
          }
        ]
      }
      me.$q.loading.show()
      contactService.contactSetAttributes(JSON.stringify(data)).then(() => {
        me.initalize()
      }, (error) => {
        console.error(error.message)
      }).then(() => {
        me.$q.loading.hide()
      })
    }
  }
}
</script>
