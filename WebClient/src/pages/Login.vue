<template>
  <q-page class="flex row i-page" style="justify-content: center;" v-if="contact === null">
<div style="display:flex;flex-direction:column">
    <div class="i-login-view" style="height: 80%">

<transition
  appear
  enter-active-class="animated slideInRight"
>
      <div class="i-login-content"  v-show="currentView === 'login'">
          <span class="i-elite q-pb-lg">THE DEN LOYALTY MEMBER</span>
        <span class="i-login-input-title"> E-MAIL </span>
        <q-input outlined stack-label square color="grey" bg-color="white" :error="$v.loginForm.email.$error"
           v-model="loginForm.email" class="" type="email" v-on:keyup.enter="loginClicked"/>
           <span class="i-login-input-title">  PASSWORD </span>
        <q-input outlined stack-label square color="grey" bg-color="white" :error="$v.loginForm.pass.$error"
           v-model="loginForm.pass" class="" type="password" v-on:keyup.enter="loginClicked"/>
        <div class="i-content-text-forgot" @click="forgotPassword">Forgot password?</div>

        <div @click="loginClicked" class="i-btn q-mt-lg">SIGN IN</div>
         <div @click="signupClicked" class="i-btn i-singup q-mt-sm">SIGN UP</div>
      </div>
</transition>
<transition
  appear
  enter-active-class="animated slideInRight"
>
      <div class="i-login-content"  v-show="currentView === 'forgot'">
        <span class="i-login-input-title"> E-MAIL </span>
        <q-input outlined stack-label square color="grey" bg-color="white" :error="$v.forgotEmail.$error"
         v-model="forgotEmail" class="" type="email"/>
        <div @click="resetPasswordClicked" class="i-btn q-mt-sm">Continue</div>
        <div class="i-content-text-forgot q-mt-lg" @click="currentView = 'login'">Back to Login</div>
      </div>
</transition>
      <div class="i-login-content"  v-show="currentView === 'sended'">
        <h5 style="color:white;padding-left: 10%;font-size:18px"> Reset password link has been sent to {{forgotEmail}}</h5>
        <div style="padding-left: 10%;" class="i-content-text-forgot q-mt-lg" @click="currentView = 'login'">Back to Login</div>

      </div>

    </div>
      <div class="row justify-start">
        <img style="width:220px;height:auto" src="statics/den-name.svg"/>
      </div>

 </div>
  </q-page>
</template>

<style>
.q-field__marginal{
  height: 40px;
}
.q-field__control{
  height:40px;
}
.i-elite{
  color:white;
  font-size: 16px;
}
.i-login-input-title{
  font-family: Arimo;
  font-style: normal;
  font-weight: normal;
  font-size: 12px;
  line-height: 14px;
  text-transform: uppercase;
  padding-bottom:3px;

/* Light gray */

color: #C7C8CA;
}

.i-page {
  font-family: Arimo;
  background-color: #333333;
  background-image:    url("/statics/CBD_Logo_TheDen_Green.png");
  background-size:     auto 90%;
  background-repeat:   no-repeat;
  background-position: right 0px center;

}
.i-btn {
  display: flex;
  justify-content: center;
  padding:10px;
  text-transform: uppercase;
  font-size: 20px;
  font-weight: bold;
  height: 40px;
  min-width: 250px;
  cursor: pointer;
  font-family:Din;
  display: flex;
  align-items: center;
  text-align: center;
  color: #58595B;
  background: #40B149;
  box-sizing: border-box;
}
.i-singup{
  margin-top:30px;
  background: #C7C8CA;
}
.i-login-view{
  display: flex;
  flex-direction: column;
  justify-content: center;
}
.i-login-content{
  display: flex;
  justify-content: center;
  padding-top:5px;
  flex-direction: column;
}
.i-register-content{
  display: flex;
  justify-content: center;
  padding-top:30px;
  flex-direction: column;
}
.i-content-register{
  text-align: center;
  font-size: 24px;
  font-weight: bold;
  cursor: pointer;
  font-family: Din;
}
.i-content-text{
  font-size:14px;
  text-align: center;
  margin-top:40px;
}
.i-content-text-forgot{
  font-size:12px;
  text-decoration: underline;
  cursor: pointer;
  color: #40B149;
}
.i-content-text-reg{
  font-size:14px;
  text-align: center;
  margin-top:5px;
  text-decoration: underline;
  cursor: pointer;
}
.t-login{
  width: 300px;
  height:400px;
  position: absolute;
  top:30%;
}
.t-forget-pass{
  cursor: pointer;
  color:#337AB7;
}

@media only screen and (max-width: 415px) {
  .i-btn{
    min-width: 222px;
  }
  .i-login-view {
    padding-right: 20%;
  }
}
</style>

<script>

import { required, email, minLength } from 'vuelidate/lib/validators'
import contactService from '../app/services/contactservice'
import userData from '../app/services/userdata'

export default {
  name: 'Login',
  components: {
  },
  data () {
    return {
      contact: null,
      currentView: 'login',
      accountID: '',
      email: '',
      pass: '',
      forgotEmail: '',
      loginForm: {
        email: '',
        pass: ''
      }
    }
  },
  validations: {
    forgotEmail: { required, email },
    loginForm: {
      email: { required, email },
      pass: { required, minLength: minLength(6) }
    }
  },
  created () {
    var me = this
    me.contact = userData.getUserData()
  },
  methods: {
    forgotPassword () {
      var me = this
      me.currentView = 'forgot'
    },
    signupClicked () {
      var me = this
      me.$router.push('/register')
    },
    resetPasswordClicked () {
      var me = this
      me.$v.forgotEmail.$touch()
      if (me.$v.forgotEmail.$error) {
        if (me.dismiss) {
          me.dismiss()
        }
        me.dismiss = me.$q.notify({
          message: 'Correct the highlighted fields',
          position: 'bottom',
          timeout: 2000,
          color: 'negative'
        })
        return
      }
      me.$q.loading.show()
      contactService.resetPassword(me.forgotEmail).then((response) => {
        if (response !== 'error') {
          me.currentView = 'sended'
        } else {
          if (me.dismiss) {
            me.dismiss()
          }
          me.dismiss = me.$q.notify({
            message: 'Invalid email',
            position: 'bottom',
            timeout: 2000
          })
        }
      }, (error) => {
        console.error(error.message)
      }).then(() => {
        me.$q.loading.hide()
      })
    },
    loginClicked () {
      var me = this
      console.log(me.$v)
      me.$v.loginForm.$touch()
      if (me.$v.loginForm.$error) {
        if (me.dismiss) {
          me.dismiss()
        }
        me.dismiss = me.$q.notify({
          message: 'Correct the highlighted fields',
          position: 'bottom',
          timeout: 2000,
          color: 'negative'
        })
        return
      }

      me.$q.loading.show()
      var loginInfo = {
        email: me.loginForm.email,
        pass: me.loginForm.pass
      }
      contactService.contactLogin(JSON.stringify(loginInfo)).then((response) => {
        me.accountID = response.accountID
        me.getContact()
      }, (error) => {
        console.error(error)
        if (me.dismiss) {
          me.dismiss()
        }
        me.dismiss = me.$q.notify({
          message: 'Invalid password or email',
          position: 'bottom',
          timeout: 2000,
          color: 'negative'
        })
      }).then(() => {
        me.$q.loading.hide()
      })
    },
    getContact () {
      var me = this
      me.$q.loading.show()
      contactService.contactGetByCardId(me.accountID).then((contact) => {
        userData.setUserData(contact)
        me.$router.push('/dashboard/home')
      }, (error) => {
        console.error(error.message)
      }).then(() => {
        me.$q.loading.hide()
      })
    }
  }
}
</script>
