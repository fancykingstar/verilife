<template>
  <q-layout view="lHh Lpr lFf">
    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>
import userData from '../app/services/userdata'

export default {
  name: 'LoginLayout',
  data () {
    return {
    }
  },
  created () {
    var me = this
    console.log('LOGIN LAYOUT!!!')
    var contact = userData.getUserData()
    if (contact) {
      me.$router.push('/dashboard/home')
    } else {
      if (!me.$q.platform.is.cordova && !me.$route.params.id) {
        // me.$router.push('/splash')
      }
    }
    if (me.$q.platform.is.cordova) {
      window.FirebasePlugin.grantPermission()
      setTimeout(function () {
        window.FirebasePlugin.getToken(function (token) {
        // save this server-side and use it to push notifications to this device
          window.FirebasePlugin.subscribe('all')
          console.log(token)
          window.FirebasePlugin.hasPermission(function (data) {
            console.log(data.isEnabled)
          })
        }, function (error) {
          console.error(error)
        })
        window.FirebasePlugin.onNotificationOpen(function (notification) {
          me.$router.push('/dashboard/notifications')
        }, function (error) {
          console.error(error)
        })
      }, 1000)
    }
  }
}
</script>

<style>

</style>
