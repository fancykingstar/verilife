<template>
  <q-page class="">
    <div class="i-contact-container">
      <div class="i-contact-body">
      <div class="i-contact-title">ACCOUNT </div>
        <q-tabs
          v-model="tab"
          dense
          class="text-grey"
          active-color="grey-6"
          indicator-color="grey-6"
          align="right"
          narrow-indicator
        >
          <q-tab name="name" label="Personal info" />
          <q-tab name="address" label="Address" />
        </q-tabs>
        <q-tab-panels v-model="tab" animated>
          <q-tab-panel class="i-tab-pane" name="name">

             <span class="i-change-input-title" style="padding-top:0px">  First Name</span>
             <q-input color="grey-5" :disable="disabled" square outlined  v-model="contact.firstName" :error="$v.contact.firstName.$error"/>

             <span class="i-change-input-title">Last Name</span>
             <q-input :disable="disabled" color="grey-5" square outlined  v-model="contact.lastName" :error="$v.contact.lastName.$error"/>

             <span class="i-change-input-title">Email</span>
             <q-input :disable="disabled" color="grey-5" square outlined  v-model="contact.email" :error="$v.contact.email.$error"/>

            <span class="i-change-input-title">Phone</span>
            <q-input :disable="disabled" color="grey-5" square outlined  v-model="contact.phone" :error="$v.contact.phone.$error"/>

            <span class="i-change-input-title">Birth Date</span>
            <q-input color="grey-5" :disable="disabled" square outlined v-model="contact.birthDay" :error="$v.contact.birthDay.$error">
              <template v-slot:append>
                <q-icon name="event" class="cursor-pointer">
                  <q-popup-proxy ref="qDateProxy" transition-show="scale" transition-hide="scale">
                    <q-date color="grey-5" :disable="disabled" v-model="contact.birthDay" @input="() => $refs.qDateProxy.hide()" />
                  </q-popup-proxy>
                </q-icon>
              </template>
            </q-input>

            <div @click="contactUpdate" class="i-btn-contact">SAVE INFO</div>

          </q-tab-panel>

          <q-tab-panel class="i-tab-pane" name="address">
            <contactaddress
            :open="addressDialogOpen"
            @close="addressDialogOpen = false"/>
          </q-tab-panel>
          </q-tab-panels>

      <div class="i-contact-line" v-if="disabled === false">
        <div class="i-contact-line-left"></div>
        <div  class="i-contact-line-right">
          <!-- <div @click="addressDialogOpen = true" class="i-btn-change">CHANGE ADDRESS</div> -->
          <div @click="passDialog = true" class="i-btn-change">CHANGE PASSWORD</div>

       </div>
      </div>
       <div class="i-contact-line">
      <div class="i-sing-out" @click="logout">Sign Out</div>
      </div>
       </div>
    </div>
     <q-dialog v-model="passDialog" :maximized="maximizedToggle"
      transition-show="slide-up"
      transition-hide="slide-down">
      <div class="i-acc-container" >
       <div class="i-acc-close">
       <q-btn icon="close" flat round dense v-close-popup />
       </div>
        <div class="i-acc-body">
          <span class="i-change-input-title">Old password</span>
        <q-input outlined stack-label square color="grey" bg-color="white" :error="$v.pssForm.oldPass.$error"
        v-model="pssForm.oldPass" class="" type="password"/>
         <span class="i-change-input-title">New password </span>
        <q-input outlined stack-label square color="grey" bg-color="white" :error="$v.pssForm.pass.$error"
        v-model="pssForm.pass" class="" type="password"/>
         <span class="i-change-input-title">Confirm Password </span>
        <q-input outlined stack-label square color="grey" bg-color="white" :error="$v.pssForm.confirmPass.$error"
       v-model="pssForm.confirmPass" class="" type="password"/>
               <div @click="changePassClicked" class="i-btn-pass q-mt-sm q-mb-lg">UPDATE</div>

        </div>
        </div>
    </q-dialog>
  </q-page>
</template>

<style>
.i-tab-pane{
  padding: 0px;
  padding-top:15px;
  overflow: hidden;
}
.i-change-input-title{
  font-family: Arimo;
  font-style: normal;
  font-weight: normal;
  line-height: 14px;
  text-transform: uppercase;
  padding-bottom:3px;
  color: #848588;
  font-size: 12px;
  padding-top:10px;
  display: block;
}
.q-field__marginal{
  height: 40px;
}
.i-btn-pass {
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
  background: #C7C8CA;
  box-sizing: border-box;
}
.q-field__control{
  height:40px;
}
.i-acc-container{
  display: flex;
  flex-direction: column;
  border:10px solid #333333;
  background: white;
  max-width: 800px!important;
}
.i-acc-close{
  display:flex;
  flex-direction: row;
  justify-content: flex-end;
  padding:3px;
}
.i-acc-body{
  padding:20px;
}
.i-contact-container{
  padding-top:10px;
  display:flex;
  flex-direction: row;
  width:100%;
  justify-content: center;
}
.i-contact-body{
  display:flex;
  flex-direction: column;
  width:90%;
}
.i-contact-container .q-field--with-bottom{
  padding-bottom: 0px!important;
}
.i-btn-change {
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
.i-btn-contact {
  display: flex;
  justify-content: center;
  padding: 5px;
  background-color: black;
  text-transform: uppercase;
  font-size: 20px;
  line-height: 30px;
  color: white;
  font-weight: bold;
  height: 40px;
  cursor: pointer;
  font-family: Din;
  margin-top: 20px;
}
.i-sing-out{
  font-family: Din;
  margin-top: 5px;
  font-size: 20px;
  color: #5d5d5d;
  font-weight: bold;
  cursor: pointer;
  width: 100%;
  text-align: center;
}
.i-contact-line-left{
  display:flex;
  flex-direction: column;
  justify-content: center;
  padding-right: 15px;
  font-family: Arimo;
  text-transform: uppercase;
  color: #848588;
  font-size: 12px;
  font-style: normal;
font-weight: normal;
}

.i-contact-line {
  display:flex;
  flex-direction: column;
  justify-content: center;
  margin-bottom: 10px;
}
.i-contact-title{
  text-align: center;
  padding: 10px;
  padding-bottom:10px;
  font-size: 20px;
  color:#58595B;
  font-family: Din;
  font-weight: bold;
}

.i-contact-container .q-field__marginal{
  height: 35px;
}
.i-contact-container .q-field__control{
  height: 35px;
}

.t-point-cont{
  display: flex;
  flex-direction: row;
}
.t-point{
  display: flex;
  justify-content: center;
  flex-direction: column;
  padding-right:20px;
  font-size: 17px;
}
/* 5000px or less */
@media only screen and (max-width: 5000px) {
  .i-contact-body{
    width:30%;
  }
}
/* 2000px or less */
@media only screen and (max-width: 2000px) {
  .i-contact-body{
    width:30%;
  }
}
/* 1000px or less */
@media only screen and (max-width: 1000px) {
.i-contact-body{
    width:40%;
  }
}
/* 600px or less */
@media only screen and (max-width: 600px) {
.i-contact-body{
    width:60%;
  }
}
/* 400px or less */
@media only screen and (max-width: 415px) {
.i-contact-body{
    width:80%;
  }
}
/* 375px or less */
@media only screen and (max-width: 380px) {

}
</style>

<script>
// import VueJsonPretty from 'vue-json-pretty'
import moment from 'moment'
import contactService from '../app/services/contactservice'
import userData from '../app/services/userdata'
import { required, email, sameAs, minLength, maxLength } from 'vuelidate/lib/validators'
import contactaddress from '../components/contactaddress'

export default {
  name: 'Contact',
  components: {
    contactaddress
  },
  data () {
    return {
      tab: 'name',
      contact: {
        firstName: '',
        middleName: '',
        lastName: '',
        email: '',
        phone: '',
        birthDay: '',
        account: {
          pointBalance: 0
        }
      },
      pssForm: {
        oldPass: '',
        pass: '',
        confirmPass: ''
      },
      disabled: false,
      maximizedToggle: this.$q.platform.is.mobile,
      passDialog: false,
      addressDialogOpen: false
    }
  },
  validations: {
    contact: {
      firstName: { required, maxLength: maxLength(50) },
      lastName: { required, maxLength: maxLength(50) },
      email: { required, email, maxLength: maxLength(50) },
      birthDay: { required, maxLength: maxLength(50) },
      phone: { maxLength: maxLength(50) }
    },
    pssForm: {
      oldPass: { required, minLength: minLength(6) },
      pass: { required, minLength: minLength(6) },
      confirmPass: { required, sameAs: sameAs('pass') }
    }
  },
  created: function () {
    var me = this
    console.log(me.$route.params.tab)
    if (me.$route.params.tab) {
      me.tab = me.$route.params.tab
    }
    var contact = userData.getUserData()
    if (contact) {
      me.setContact(contact)
    }
  },
  methods: {
    changePassClicked () {
      var me = this
      me.$v.pssForm.$touch()
      if (me.$v.pssForm.$error) {
        var txt = ''
        if (me.pssForm.pass.length < 6) {
          txt += 'Minimum password length is 6 symbols.'
        }
        if (me.dismiss) {
          me.dismiss()
        }
        me.dismiss = me.$q.notify({
          message: 'Correct the highlighted fields. ' + txt,
          position: 'bottom',
          timeout: 3000,
          color: 'negative',
          multiLine: false
        })
        return
      }
      contactService.contactChangePassword(me.contact.id, me.pssForm.oldPass, me.pssForm.pass).then((res) => {
        if (res !== 'success') {
          me.$q.notify({
            message: res,
            position: 'bottom',
            timeout: 3000,
            color: 'negative'
          })
        } else {
          me.passDialog = false
          me.$q.notify({
            color: 'grey-6',
            position: 'bottom',
            message: 'Password has been updated',
            icon: 'done'
          })
        }
      }, (error) => {
        console.error(error.message)
      })
    },
    setContact (contact) {
      var me = this
      me.contact = contact
      var a = moment()
      var b = moment(me.contact.birthDay)
      if (a.diff(b, 'years') > 100) {
        me.contact.birthDay = ''
      } else {
        me.contact.birthDay = moment(me.contact.birthDay.split('T')[0]).format('YYYY/MM/DD')
      }
      if (me.contact.account.scheme.club.id === '200') {
        me.disabled = true
      }
    },
    logout () {
      var me = this
      userData.removeUserData()
      me.$router.push('/')
    },
    contactUpdate () {
      var me = this
      me.$v.contact.$touch()
      if (me.$v.contact.$error) {
        me.$q.notify({
          message: 'Correct the highlighted fields',
          position: 'bottom',
          timeout: 2000,
          color: 'negative'
        })
        return
      }
      var updatedContact = {
        Email: me.contact.email,
        FirstName: me.contact.firstName,
        MiddleName: me.contact.middleName,
        LastName: me.contact.lastName,
        Phone: me.contact.phone,
        Id: me.contact.id,
        birthDay: me.contact.birthDay
      }
      me.$q.loading.show()
      contactService.contactUpdate(updatedContact).then((response) => {
        if (response !== 'success') {
          me.$q.notify({
            message: response,
            position: 'bottom',
            timeout: 2000,
            color: 'negative'
          })
          me.$q.loading.hide()
          return
        }
        contactService.contactGetByCardId(me.contact.id).then((contact) => {
          me.setContact(contact)
          userData.setUserData(contact)
          me.$q.loading.hide()
        }, (error) => {
          console.error(error.message)
          me.$q.loading.hide()
        })
      }, (error) => {
        me.$q.notify({
          message: error.data.error,
          position: 'bottom',
          timeout: 2000,
          color: 'negative'
        })
        me.$q.loading.hide()
        console.error(error.message)
      })
    }
  }
}
</script>
