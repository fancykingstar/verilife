<template>

  <q-page class="flex row i-page" style="justify-content: center;">

    <div class="i-login-view">
      <transition
      appear
      enter-active-class="animated slideInRight"
    >
      <div class="i-login-content"  v-show="page === 'codesent'">
        <h5 style="color:white;padding-left: 10%;font-size:18px"> Signup code has been sent to {{externalID}} </h5>
        <div style="padding-left: 10%;" class="i-content-text-forgot q-mt-lg" @click="backToLogin">Back to Login</div>
      </div>
    </transition>

    <transition
      appear
      enter-active-class="animated slideInRight"
    >
      <div style="width: 280px;" class="i-login-content" v-show="page === 'register' && isVerifed === false">
        <span style="text-transform: capitalize;" class="i-login-input-title">Device verification code</span>
        <q-input style="margin-bottom: 20px;" outlined stack-label square color="grey" bg-color="white" v-model="verifyCode" class="" type="text"/>
        <q-btn color="secondary" label="Verify" @click="verifyEmail" />
        <div style="color: #C7C8CA; word-break: break-all; margin-top: 10px">
          We just sent your authentication code via email to {{alertEmail}}.
        </div>
      </div>
    </transition>

    <transition
      appear
      enter-active-class="animated slideInRight"
    >
      <div class="i-login-content"  v-show="page === 'register' && isVerifed === true">
            <span class="i-login-input-title"> NAME </span>
            <q-input outlined stack-label square color="grey" bg-color="white" :error="$v.regFormNew.name.$error"
            v-model="regFormNew.name" class="" type="text"/>
            <span class="i-login-input-title"> E-MAIL </span>
            <q-input outlined stack-label square color="grey" bg-color="white" :error="$v.regFormNew.email.$error"
            v-model="regFormNew.email" class="" type="email"/>
            <span class="i-login-input-title"> PASSWORD </span>
            <q-input outlined stack-label square color="grey" bg-color="white" :error="$v.regFormNew.pass.$error"
            v-model="regFormNew.pass" class="" type="password"/>
            <span class="i-login-input-title"> CONFIRM PASSWORD </span>
            <q-input outlined stack-label square color="grey" bg-color="white" :error="$v.regFormNew.confirmPass.$error"
            v-model="regFormNew.confirmPass" class="" type="password"/>
            <div class="text-white">
              <q-checkbox dark color='blue' v-model="age" label="I am 19 years or older" />
            </div>
            <div class="text-white">
              <q-checkbox dark color='blue' v-model="term" label="I agree with terms and conditions" />
            </div>
            <div :disabled="!term || !age" @click="registerButtonClicked" class="i-btn-r q-mt-sm">REGISTER</div>
            <div class="i-content-text-forgot q-mt-lg" @click="backToLogin">Back to Login</div>
      </div>
    </transition>

<transition
  appear
  enter-active-class="animated slideInRight"
>
      <div class="i-login-content"  v-show="account === null && page === ''">
           <span class="i-login-input-title"> Enter your e-mail or signup code </span>

        <q-input outlined stack-label square color="grey" bg-color="white" :error="$v.externalID.$error"
           v-model="externalID" class="q-mb-sm"/>
        <div @click="loginClicked" class="i-btn-r q-mt-sm">CONTINUE</div>
        <div class="i-content-text-forgot q-mt-lg" @click="backToLogin">Back to Login</div>

      </div>
</transition>
<transition
  appear
  enter-active-class="animated slideInRight"
>
      <div class="i-register-content"  v-show="account !== null && page === ''">
        <span class="i-login-input-title">E-MAIL </span>
        <q-input outlined stack-label square color="grey" bg-color="white" :error="$v.regForm.email.$error"
        v-model="regForm.email" class="" type="email"/>
         <span class="i-login-input-title">Password </span>
        <q-input outlined stack-label square color="grey" bg-color="white" :error="$v.regForm.pass.$error"
        v-model="regForm.pass" class="" type="password"/>
         <span class="i-login-input-title">Confirm Password </span>
        <q-input outlined stack-label square color="grey" bg-color="white" :error="$v.regForm.confirmPass.$error"
       v-model="regForm.confirmPass" class="" type="password"/>
        <div @click="registerContinue" class="i-btn-r q-mt-sm">CONTINUE</div>
      </div>
</transition>

    </div>

  </q-page>
</template>

<style>
.i-content-text-forgot{
  font-size:12px;
  text-decoration: underline;
  cursor: pointer;
  color: white;
}
.q-field__marginal{
  height: 40px;
}
.q-field__control{
  height:40px;
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
  background-color: #000000;
  background-image:    url("/statics/veri-logo.png");
  background-size:     auto 90%;
  background-repeat:   no-repeat;
  background-position: right 0px center;
}
.i-btn-r {
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
  background: #dadada;
  box-sizing: border-box;
  user-select: none;
}
.i-login-view{
  display: flex;
  flex-direction: column;
  justify-content: center;
}
.i-login-content{
  display: flex;
  justify-content: center;
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
  font-size:14px;
  text-align: center;
  margin-top:30px;
  text-decoration: underline;
  cursor: pointer;
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
    /* padding-right: 20%; */
  }
}
</style>

<script>
import contactService from '../app/services/contactservice'
import { required, email, sameAs, minLength } from 'vuelidate/lib/validators'
import userData from '../app/services/userdata'

export default {
  name: 'Regiser',
  components: {
  },
  data () {
    return {
      page: '',
      term: false,
      age: false,
      verifyCode: '',
      isVerifed: false,
      account: null,
      externalID: '',
      code: '',
      alertEmail: '',
      regForm: {
        name: '',
        email: '',
        pass: '',
        confirmPass: ''
      },
      regFormNew: {
        name: '',
        email: '',
        pass: '',
        confirmPass: ''
      }
    }
  },
  validations: {
    externalID: { required },
    regFormNew: {
      name: { required },
      email: { required, email },
      pass: { required, minLength: minLength(6) },
      confirmPass: { required, sameAs: sameAs('pass'), minLength: minLength(6) }
    },
    regForm: {
      email: { required, email },
      pass: { required, minLength: minLength(6) },
      confirmPass: { required, sameAs: sameAs('pass') }
    }
  },
  created () {
    var me = this
    me.externalID = me.$route.params.id
    if (me.externalID === 'register') {
      me.externalID = ''
    } else {
      me.loginClicked()
    }
  },
  methods: {
    backToLogin () {
      var me = this
      me.$router.push('/login')
    },
    emailCheckRegistration () {
      var me = this
      me.$q.loading.show()
      contactService.contactCheckByEmail(me.externalID, me.code).then((response) => {
        var leng = me.externalID.length
        var start = me.externalID.slice(0, 1)
        var end = '@' + me.externalID.split('@')[1]
        var i = 0
        var asto = ''
        while (i < leng - end.length - 1) {
          asto = asto + '*'
          i++
        }
        me.alertEmail = start + asto + end
        if (response === 'Not exist') {
          me.regFormNew.email = me.externalID
          me.page = 'register'
        } else {
          me.page = 'codesent'
        }
      }, (error) => {
        console.error(error.message)
      }).then(() => {
        me.$q.loading.hide()
      })
    },
    loginClicked () {
      var me = this
      var min = 100000
      var max = 999999
      me.code = Math.floor(Math.random() * (max - min + 1) + min)

      me.$v.externalID.$touch()
      if (me.$v.externalID.$error) {
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
      if (me.externalID.indexOf('@') !== -1) {
        me.emailCheckRegistration()
        return
      }
      me.$q.loading.show()
      contactService.getContactByExternalID(me.externalID.toUpperCase()).then((contact) => {
        if (contact === 'Contact not found') {
          me.$q.notify({
            message: 'Contact not found',
            position: 'bottom',
            timeout: 2000,
            color: 'negative'
          })
          return
        }
        if (contact === 'Blocked') {
          me.$q.notify({
            message: 'Too many attempts. Blocked for 15 min',
            position: 'bottom',
            timeout: 2000,
            color: 'negative'
          })
          return
        }
        console.log(contact)
        me.account = contact
        me.regForm.email = contact.email
      }, (error) => {
        console.error(error.message)
      }).then(() => {
        me.$q.loading.hide()
      })
    },
    login () {
      var me = this
      me.$q.loading.show()
      contactService.contactGetByCardId(me.account.id).then((contact) => {
        userData.setUserData(contact)
        me.$router.push('/dashboard/home')
      }, (error) => {
        console.error(error.message)
      }).then(() => {
        me.$q.loading.hide()
      })
    },
    verifyEmail () {
      var me = this
      if (me.code.toString() === me.verifyCode) {
        me.isVerifed = true
      } else {
        if (me.dismiss) {
          me.dismiss()
        }
        me.dismiss = me.$q.notify({
          message: 'Incorrect verification code provided.',
          position: 'top',
          timeout: 3000,
          color: 'negative'
        })
      }
    },
    registerContinue () {
      var me = this
      me.$v.regForm.$touch()
      if (me.$v.regForm.$error) {
        var txt = ''
        if (me.regForm.pass.length < 6) {
          txt += 'Minimum password length is 6 symbols.'
        }
        if (me.dismiss) {
          me.dismiss()
        }
        me.dismiss = me.$q.notify({
          message: 'Correct the highlighted fields. ' + txt,
          position: 'bottom',
          timeout: 3000,
          color: 'negative'
        })
        return
      }
      me.$q.loading.show()
      var registerInfo = {
        id: me.account.id,
        email: me.regForm.email,
        pass: me.regForm.pass
      }
      contactService.contactRegister(JSON.stringify(registerInfo)).then((response) => {
        if (response.error) {
          me.$q.notify({
            message: response.error,
            position: 'bottom',
            timeout: 2000,
            color: 'negative'
          })
        } else {
          me.login()
        }
      }, (error) => {
        console.error(error.message)
      }).then(() => {
        me.$q.loading.hide()
      })
    },
    registerButtonClicked () {
      var me = this
      if (me.term && me.age) {
        me.$v.regFormNew.$touch()
        if (me.$v.regFormNew.$error) {
          var txt = ''
          if (me.regFormNew.pass.length < 6) {
            txt += 'Minimum password length is 6 symbols.'
          }
          if (me.dismiss) {
            me.dismiss()
          }
          me.dismiss = me.$q.notify({
            message: 'Correct the highlighted fields. ' + txt,
            position: 'bottom',
            timeout: 3000,
            color: 'negative'
          })
          return
        }
        me.$q.loading.show()
        contactService.contactRegisterByEmail(me.regFormNew.name, me.regFormNew.email, me.regFormNew.pass).then((response) => {
          if (response.error) {
            if (me.dismiss) {
              me.dismiss()
            }
            me.dismiss = me.$q.notify({
              message: response.error,
              position: 'bottom',
              timeout: 3000,
              color: 'negative'
            })
            return
          }
          me.account = {
            id: response.accountID
          }
          me.login()
        }, (error) => {
          if (me.dismiss) {
            me.dismiss()
          }
          me.dismiss = me.$q.notify({
            message: error.data.error,
            position: 'bottom',
            timeout: 3000,
            color: 'negative'
          })
        }).then(() => {
          me.$q.loading.hide()
        })
      }
    }
  }
}
</script>
