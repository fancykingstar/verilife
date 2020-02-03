<template>
<div class="i-home-main">
    <q-pull-to-refresh
      @refresh="refresh"
      color="black"
    >
  <q-resize-observer @resize="onResize" />
  <div class="i-home-container i-top">
    <div class="i-home-body">

      <div class="i-name-row">
        <div class="i-name-row-left">
          <div class="i-home-name"> Welcome back</div>
          <div class="i-name-title">{{contact.name}}</div>
            <div class="i-name-subtitle q-pt-sm">{{clubCode}} Member</div>
            <!-- <div v-if="schemeCode === '1'" class="i-name-subtitle q-pt-sm">Silver Member</div>
            <div v-if="schemeCode === '2'" class="i-name-subtitle q-pt-sm">Platinum Member</div> -->
          <div class="i-name-subtitle" style="color:#58595b">{{contact.id}} {{location}}</div>
        </div>
        <div class="i-name-row-right">
          <div class="i-name-diamond"><img :src="getImageUrl(schemeCode)" />
            <!-- <img v-if="schemeCode === '1'" src="statics/Silver_white_shadow.png"/>
            <img v-if="schemeCode === '2'" src="statics/Platinum_white_shadow.png"/> -->
          </div>
        </div>
      </div>

      <div class="i-balance-row">
        <div class="i-balance-left">
          <qrcode :value="contact.id" class="i-qr-box" :options="{ width: qrsize, margin: 1, displayValue: false }"></qrcode>
        </div>
        <div class="i-balance-right">
          <div class="i-balance-container">
            <div class="i-balance-container-title-top">Member since <br />{{createdDate}}</div>
            <div class="i-balance-container-title-bottom"><span>point balance</span></div>
            <div class="i-balance-container-point" v-if="contact.account">{{contact.account.pointBalance}}</div>
          </div>
        </div>
      </div>
      <a href="#prom-id" style="text-decoration: none;" v-smooth-scroll="{ duration: 1000, offset: -90}">
      <div class="i-home-arrow-down">
         <q-btn
         outline
        rounded
        dense
        color="white"
        size="md"
        icon="keyboard_arrow_down"
        ></q-btn>
      </div></a>
  </div>
  </div>
  <div id="prom-id"></div>
  <div v-if="featuredWatch && featuredWatch.description" >
    <div class="i-home-container i-padding-none" >
      <div class="i-home-body i-bottom">

        <div class="i-offer-row" @click="gotoFeaturedLink(featuredWatch)">
          <q-img
          class="i-offer-img2"
            :src="getImageUrl(featuredWatch.code)"
            spinner-color="grey-5"
            />
        </div>
      </div>
    </div>
  </div>
   <div class="i-home-container i-bottom">
    <div class="i-home-body i-bottom">
      <div class="i-home-offer-title"><h2>Promotions</h2></div>

       <div class="i-offer-row">
        <offers ref="off"></offers>
      </div>
    </div>
  </div>
  </q-pull-to-refresh>
</div>
</template>

<style>
.i-home-container.i-padding-none {
  padding-bottom:0px;
  padding-top:0px;
  background-image:none;
  background: black;
}
.i-home-arrow-down .q-btn--outline {
    border: 3px solid currentColor!important;
    font-size: 15px;
    opacity: 0.5;
}
.i-home-arrow-down{
  display: flex;
    justify-content: center;
    padding-top: 17px;
}
.i-home-offer-title{
  display: flex;
    justify-content: center;
    padding: 10px 0px;
}
.i-home-offer-title h2{
  text-transform: uppercase;
  color:black;
  border-top: 2px solid black;
  border-bottom: 2px solid black;
  font-family: Gotham;
  font-size: 24px;
  font-weight: bold;
}
.i-home-container.i-top {
   padding-bottom:20px;
}
.i-home-container.i-bottom {
   padding-bottom: 15px;
   background-color: #e4e4e4;
}
.i-home-separator{
  height:30px;
background-image: linear-gradient(135deg, #000000 40%, #575757 40%, #575757 50%, #000000 50%, #000000 90%, #575757 90%, #575757 100%);
background-size: 14.14px 14.14px;
}
.i-home-container{
background:#000000;
  background-repeat:   no-repeat;
  background-position: center center;
}
.i-offer-row{
  display: flex;
}
.i-home-name{
  padding: 10px;
  padding-left:0px;
  font-size: 13px;
    color: white;
  text-shadow: 7px 7px 6px rgba(0,0,0,0.5);
  text-transform: uppercase;
}
.i-balance-container-point{
  font-size: 60px;
  font-weight: bold;
  line-height: 60px;
  margin-bottom: 3px;
  color:white;
  text-shadow: 7px 7px 6px rgba(0,0,0,0.5);
  border-bottom: 8px solid #58595b;
  padding-top: 10px;
  padding-bottom: 10px;
}
.i-balance-container-title-bottom{
  text-transform: uppercase;
  color:white;
  font-size: 18px;
  font-family: Gotham;
  font-weight: 100;
  border-bottom:8px solid #58595b;
  padding: 5px 0;
}
.i-balance-container-title-top{
  text-transform: uppercase;
  color:white;
  font-size: 13px;
  border-bottom: 8px solid #58595b;
  padding: 10px 0px;
  font-family: "Museo Sans";
  font-weight: 100;
}
.i-balance-container-title-bottom span{
}
.i-balance-container{
  text-align: center;
  width: 170px;
}
.i-qr-box{
  align-self: center;
  background: white;
  padding: 10px;
  box-shadow: 20px 20px 20px rgba(0,0,0,0.7);
}
.i-balance-left{
  width:70%;
}
.i-balance-right{
  display: flex;
  flex-direction: column;
  display: flex;
  justify-content: center;
}
.i-balance-row{
  margin-top:20px;
  display:flex;
  flex-direction: row;
}
.i-name-diamond{
  display: flex;
  float: none;
  width: 170px;
  height: 100px;
  justify-content: center;
}
.i-name-diamond img{
  height:100%;
}
.i-name-subtitle{
  text-transform: uppercase;
    color: white;
  text-shadow: 7px 7px 6px rgba(0,0,0,0.5);
  font-size: 13px;
}
.i-name-title{
  color: white;
  text-shadow: 7px 7px 6px rgba(0,0,0,0.5);
  font-size: 30px;
  line-height: 100%;
  text-transform: uppercase;
}
.i-name-row-right{
  display: flex;
  flex-direction: column;
  justify-content: center;
}
.i-name-row-left{
  width:70%;
}
.i-name-row{
  display:flex;
  flex-direction: row;
}
.i-home-container{
  padding-top:10px;
  display:flex;
  flex-direction: row;
  justify-content: center;
}
.i-home-body{
  width:60%;
  font-family: Din;
  font-weight: bold;
}
.i-home-body.i-bottom {
  width: 100%;
}
.i-qr-box{
  padding:5px;
}
/* 3000px or less */
@media only screen and (max-width: 5000px) {
  .i-home-body{
    width:30%;
  }
}
/* 2000px or less */
@media only screen and (max-width: 2000px) {
  .i-home-body{
    width:50%;
  }
}
/* 1000px or less */
@media only screen and (max-width: 1000px) {
  .i-home-body{
    width:70%;
  }
    .i-home-body.i-bottom{
  width:100%!important;
}
}
/* 600px or less */
@media only screen and (max-width: 600px) {
  .i-home-body{
    width:85%;
  }

}
/* 400px or less */
@media only screen and (max-width: 415px) {
 .i-qr-box{
    padding:10px;
  }
  .i-balance-left{
    display: flex;
    width: 100%;
        justify-content: center;
  }
  .i-balance-row{
    flex-direction: column;
  }
  .i-balance-right{
    margin-top:10px
  }
  .i-name-diamond{
    width:100%;
  }
  .i-balance-container{
    align-self: center;
    margin-top:10px;
  }
  .i-home-container.i-top {
   min-height: calc(100vh - 70px);
}

}
/* 375px or less */
@media only screen and (max-width: 380px) {

}
</style>

<script>
import VueQrcode from '@chenfengyuan/vue-qrcode'
import contactService from '../app/services/contactservice'
import moment from 'moment'
import offers from '../components/Offers'
import EventBus from '../app/globalbus'
import _ from 'lodash'
import userData from '../app/services/userdata'

export default {
  name: 'Home',
  components: {
    qrcode: VueQrcode,
    offers: offers
  },
  data () {
    return {
      contact: {
        id: ''
      },
      clubCode: '', // 100 - customers, 200 - employees
      schemeCode: 'MEDICAL', // 1 - silver, 2 - gold,
      lastPurchaseDate: '',
      lastPurshasePoints: '',
      schemeExpirationDate: '',
      createdDate: '',
      qrsize: 200,
      location: '',
      featuredWatch: null
    }
  },
  created: function () {
    var me = this
    me.initalize()
  },
  methods: {
    gotoFeaturedLink (featured) {
      window.location = featured.description
    },
    getImageUrl (id) {
      return process.env.API + '/images/' + id
    },
    refresh (done) {
      var me = this
      me.initalize(done)
      me.$refs.off.refresh()
    },
    initalize (done) {
      var me = this
      var contact = userData.getUserData()
      // me.schemeCode = contact.account.scheme.id
      if (contact) {
        me.contact = contact
        contactService.contactGetByCardId(me.contact.account.id).then((contact) => {
          userData.setUserData(contact)
          me.contact = contact
          me.schemeCode = me.contact.account.scheme.id
          me.clubCode = me.contact.account.scheme.club.id
          var contactExtInfo = JSON.parse(me.contact.externalInfo)
          me.lastPurchaseDate = moment(contactExtInfo.lastPurchaseDate).format('MMMM Do YYYY')
          if (contactExtInfo.lastPurchaseDate === '0001-01-01T00:00:00') {
            me.lastPurchaseDate = ''
          }
          me.schemeExpirationDate = contactExtInfo.schemeExpirationDate
          me.lastPurshasePoints = contactExtInfo.lastPurshasePoints
          me.createdDate = moment(contactExtInfo.createdDate).format('MM.DD.YYYY')
          /*
          if (contactExtInfo.companyName === 'Retailing Enterprises, LLC') {
            me.location = 'US'
          } else {
            me.location = 'PR'
          }
          */
          console.log(me.schemeCode + ' ' + me.clubCode)
          if (done) {
            done()
          }
        }, (error) => {
          console.error(error.message)
        }).then(() => {
          me.$q.loading.hide()
        })

        contactService.notificationsGetByContactId(me.contact.account.id).then((notifications) => {
          var readNoti = localStorage.getItem('READ_NOTILIST')
          var count = 0
          if (readNoti) {
            var readNotiArr = JSON.parse(readNoti)
            _.each(notifications, (n) => {
              var f = _.find(readNotiArr, (readed) => { return readed === n.code })
              if (!f) {
                count = count + 1
              }
            })
          } else {
            count = notifications.length
          }
          EventBus.$emit('NOT_UPDATED', count)
        }, (error) => {
          console.error(error.message)
        })
        contactService.getFeaturedWatch(me.contact.id).then((featured) => {
          console.log('featured')
          me.featuredWatch = featured
          console.log(featured)
        }, (error) => {
          console.error(error.message)
        })
      } else {
        me.$router.push('/')
      }
    },
    onResize (size) {
      var me = this
      me.qrsize = 150
      if (size.width < 415 && window.innerHeight > 735) {
        me.qrsize = size.width * 0.65
        console.log(me.qrsize)
      }
      if (size.width < 415 && window.innerHeight > 800) {
        me.qrsize = size.width * 0.70
        console.log(me.qrsize)
      } if (size.width < 380 && window.innerHeight < 800) {
        me.qrsize = size.width * 0.55
        console.log(me.qrsize)
      }
    }
  }
}
</script>
