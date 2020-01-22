<template>
  <q-layout view="lHh Lpr lFf">
    <q-resize-observer @resize="onResize" />
    <div v-if="isMobile === true">
      <q-header elevated>
        <div class="i-toolbar">
          <q-toolbar class="no-shadow">
            <div>
            <img class="" src="statics/logo-stories.png" style="height:30px"/>
            </div>
            <q-toolbar-title>
            </q-toolbar-title>
              <q-btn flat round dense icon="notifications_none"  @click="gotoNot">
                <q-badge v-if="notiCaount" color="red" floating>{{notiCaount}}</q-badge>
              </q-btn>
          </q-toolbar>
        </div>
      </q-header>
      <q-footer bordered class="bg-white text-primary">
          <q-tabs :breakpoint="500" no-caps active-color="grey-8"
          indicator-color="transparent" class="i-mobile-tabs" v-model="tab"  align="justify">
              <q-route-tab class="i-route-tab" v-show="tab!=='home'"
                icon="img:statics/home-inactive.svg"
                to="/dashboard/home"
                label="HOME"
                exact
                name="home"
              />
              <q-route-tab class="i-route-tab" v-show="tab==='home'"
                icon="img:statics/home-active.svg"
                to="/dashboard/home"
                label="HOME"
                exact
                name="home"
              />
             <!-- <q-route-tab class="i-route-tab"
                icon="img:statics/icon-offers.svg"
                to="/dashboard/offers"
                label="OFFERS"
                exact
              /> -->
              <q-route-tab class="i-route-tab" v-show="tab!=='history'"
                icon="img:statics/history-inactive.svg"
                to="/dashboard/history"
                label="HISTORY"
                exact
                name="history"
              />
              <q-route-tab class="i-route-tab" v-show="tab==='history'"
                icon="img:statics/history-active.svg"
                to="/dashboard/history"
                label="HISTORY"
                exact
                name="history"
              />
             <!--  <q-route-tab class="i-route-tab"
                icon="img:statics/icon-collections.svg"
                to="/dashboard/collections"
                label="COLLECTIONS"
                exact
              /> -->
              <q-route-tab class="i-route-tab"  v-show="tab!=='account'"
                icon="img:statics/account-inactive.svg"
                to="/dashboard/contact"
                label="ACCOUNT"
                exact
                 name="account"
              />
              <q-route-tab class="i-route-tab" v-show="tab==='account'"
                icon="img:statics/account-active.svg"
                to="/dashboard/contact"
                label="ACCOUNT"
                exact
                 name="account"
              />

          </q-tabs>
      </q-footer>
    </div>
    <div v-if="isMobile === false">
          <q-header elevated>
      <div class="i-toolbar">
        <q-toolbar class="no-shadow">
          <q-btn
            flat
            dense
            round
            @click="leftDrawerOpen = !leftDrawerOpen"
            aria-label="Menu"
            class="q-ml-sm"
          >
            <q-icon name="menu" />
          </q-btn>

          <q-toolbar-title style="tex-align:center">
            <div class="i-tool-logo">
              <img src="statics/den-logo-white.svg"/>
            </div>
          </q-toolbar-title>
            <q-btn class="q-mr-sm" flat round dense icon="notifications_none" style="" @click="gotoNot">
             <q-badge v-if="notiCaount" color="red" floating>{{notiCaount}}</q-badge>
            </q-btn>
        </q-toolbar>
      </div>
    </q-header>

    <q-drawer
      v-model="leftDrawerOpen"
      bordered
      content-class="i-left-menu"
    >
      <q-list>
        <q-item-label header>
          <img class="i-drawer-logo" src="statics/den-logo-leaf.png"/>
        </q-item-label>

        <q-item clickable to="/dashboard/home">
          <q-item-section avatar>
            <img   class="q-ml-md" src="statics/home-active.svg"/>
          </q-item-section>
          <q-item-section>
            <q-item-label class="i-menu-text">Home</q-item-label>
          </q-item-section>
        </q-item>

       <!--  <q-separator spaced class="i-drawer-separator" color="grey-4"/>

        <q-item clickable to="/dashboard/offers">
          <q-item-section avatar>
           <img class="q-ml-md" src="statics/icon-offers.svg"/>
          </q-item-section>
          <q-item-section>
            <q-item-label class="i-menu-text">Offers</q-item-label>
          </q-item-section>
        </q-item> -->

        <q-separator spaced class="i-drawer-separator" color="grey-4"/>

        <q-item clickable to="/dashboard/history">
          <q-item-section avatar>
            <img  class="q-ml-md" src="statics/history-active.svg"/>
          </q-item-section>
          <q-item-section>
            <q-item-label class="i-menu-text">History</q-item-label>
          </q-item-section>
        </q-item>

      <!--   <q-separator spaced class="i-drawer-separator" color="grey-4"/>

        <q-item clickable to="/dashboard/collections">
          <q-item-section avatar>
            <img  class="q-ml-md" src="statics/icon-collections.svg"/>
          </q-item-section>
          <q-item-section>
            <q-item-label class="i-menu-text">Collections</q-item-label>
          </q-item-section>
        </q-item>

         <q-separator spaced class="i-drawer-separator" color="grey-4"/>

      <q-item clickable to="/dashboard/contact">
          <q-item-section avatar>
           <img class="q-ml-md" src="statics/icon-addresses.svg"/>
          </q-item-section>
          <q-item-section>
            <q-item-label class="i-menu-text">Addresses</q-item-label>
          </q-item-section>
        </q-item> -->
        <q-separator spaced class="i-drawer-separator" color="grey-4"/>

        <q-item clickable to="/dashboard/contact">
          <q-item-section avatar>
           <img class="q-ml-md" src="statics/account-active.svg"/>
          </q-item-section>
          <q-item-section>
            <q-item-label class="i-menu-text">Account</q-item-label>
          </q-item-section>
        </q-item>

      <q-separator spaced class="i-drawer-separator" color="grey-4"/>
<!--
        <q-item clickable to="/dashboard/qrcode">
          <q-item-section avatar>
            <q-icon name="credit_card" />
          </q-item-section>
          <q-item-section>
            <q-item-label>QR code</q-item-label>
            <q-item-label caption>Discount code</q-item-label>
          </q-item-section>
        </q-item>
-->

<!--
        <q-item clickable to="/dashboard/notifications">
          <q-item-section avatar>
            <q-icon name="error_outline" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Notifications</q-item-label>
            <q-item-label caption>Notification list</q-item-label>
          </q-item-section>
        </q-item>

          <q-item clickable @click="logoff">
          <q-item-section avatar>
            <q-icon name="power_settings_new" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Logout</q-item-label>
          </q-item-section>
        </q-item>
-->
      </q-list>
    </q-drawer>
    </div>
    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>
import { openURL } from 'quasar'
import EventBus from '../app/globalbus'
import userData from '../app/services/userdata'

export default {
  name: 'MyLayout',
  data () {
    return {
      tab: '',
      leftDrawerOpen: this.$q.platform.is.desktop,
      isMobile: false,
      notiCaount: null
    }
  },
  created: function () {
    var me = this
    var contact = userData.getUserData()
    if (contact) {
      me.contact = contact
    } else {
      me.$router.push('/')
    }
    EventBus.$on('NOT_UPDATED', function (number) {
      me.notiCaount = number
    })
  },
  methods: {
    gotoNot () {
      console.log('clicked notif')
      this.$router.push('/dashboard/notifications')
    },
    openURL,
    logoff () {
      this.$router.push('/')
    },
    onResize (size) {
      /*
      var me = this
      if (this.$q.platform.is.cordova) {
        me.isMobile = true
      } else {
        me.isMobile = false
      }
      // me.isMobile = true
      */
    }
  }
}
</script>

<style>
.q-notification__message {
  text-align: center;
}
.i-tool-logo{
  display: flex;
    text-align: center;
    justify-content: center;
}
.i-tool-logo img {
  height:60px;
}
.q-header {
  padding-top: constant(safe-area-inset-top);
  padding-top: env(safe-area-inset-top);
  background:  black;
  color: white;
}
.q-footer .q-tab--full{
  min-height: calc(72px + constant(safe-area-inset-top)/3);
  min-height: calc(72px + env(safe-area-inset-top)/3);
  padding-bottom: calc(constant(safe-area-inset-top)/3)!important;
  padding-bottom: calc(env(safe-area-inset-top)/3)!important;
}
.q-footer .q-tab__label{
  margin-top:2px;
}
.i-mobile-tabs {
  background: #333333;
  color:white;
}

.i-mobile-tabs .q-router-link--active{
  color:black!important;
}
a.q-tab--active{
  background:  #FFF200;

}
.i-mobile-tabs .q-tab {
  width: calc(100vw/3);
  padding: 0px;
}
.i-route-tab .q-tab__icon{
    width: 50px;
    height: 20px;
}
.i-loyalty-txt{
  color: #58595B;
  font-size: 10px;
  text-align: center;
}
.i-left-menu{
  background: #C4C4C4;
}

.i-drawer-logo {
  width:auto;

      height: 50px;
    padding-left: 15px;
}
.i-drawer-separator{
  margin-left:30px;
width:80%;
}
.i-menu-text{
  font-size: 20px;
  text-transform: uppercase;
  color: #333333;
}
a.q-item.q-router-link--active{
  background-color: #E1E1E1;
}

</style>
