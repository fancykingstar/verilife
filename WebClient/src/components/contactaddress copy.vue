<template>
     <q-dialog v-model="dialogOpen" :maximized="maximizedToggle" @hide="$emit('close')"
      transition-show="slide-up"
      transition-hide="slide-down">
      <div class="i-acc-container" >
       <div class="i-acc-close">
       <q-btn icon="close" flat round dense v-close-popup />
       </div>
        <div class="i-acc-body">

<!-- <span class="i-change-input-title">Post Code</span>
              <q-select
                new-value-mode="add"
                hide-dropdown-icon
                v-model="model"
                use-input
                hide-selected
                fill-input
                input-debounce="0"
                :options="options"
                @filter="filterFn"
                outlined
                square color="grey"
                bg-color="white"
                @popup-hide="autohide"
              >
                <template v-slot:no-option>
                  <q-item>
                    <q-item-section class="text-grey">
                      No results
                    </q-item-section>
                  </q-item>
                </template>
              </q-select>

<div style="height:15px;"/>
-->
        <span class="i-change-input-title">Post Code</span>
          <q-input outlined stack-label square color="grey"
            bg-color="white" :error="$v.addressForm.postCode.$error"
          v-model="addressForm.postCode" class=""/>

          <span class="i-change-input-title">Address</span>
          <q-input outlined stack-label square color="grey"
            bg-color="white" :error="$v.addressForm.address1.$error"
          v-model="addressForm.address1" class=""/>

         <!--  <span class="i-change-input-title">Address 2</span>
          <q-input outlined stack-label square color="grey"
            bg-color="white" :error="$v.addressForm.address2.$error"
          v-model="addressForm.address2" class=""/> -->

          <span class="i-change-input-title">City</span>
          <q-input outlined stack-label square color="grey"
            bg-color="white" :error="$v.addressForm.city.$error"
          v-model="addressForm.city" class=""/>

           <span class="i-change-input-title">State</span>
          <q-input outlined stack-label square color="grey"
            bg-color="white" :error="$v.addressForm.stateProvinceRegion.$error"
          v-model="addressForm.stateProvinceRegion" class=""/>

           <span class="i-change-input-title">Country</span>
          <q-input outlined stack-label square color="grey"
            bg-color="white" :error="$v.addressForm.country.$error"
          v-model="addressForm.country" class=""/>

        <div @click="saveClicked" class="i-btn-contact q-mt-sm q-mb-lg">SAVE</div>

        </div>
        </div>
    </q-dialog>
</template>

<style>
.q-field--auto-height .q-field__control, .q-field--auto-height .q-field__native {
    min-height: 40px;
}
</style>

<script>
import { required } from 'vuelidate/lib/validators'
import userData from '../app/services/userdata'
import contactService from '../app/services/contactservice'
import _ from 'lodash'

export default {
  name: 'contactaddress',
  props: ['open'],

  components: {
  },
  data () {
    return {
      model: null,
      options: [],
      loadedCodes: [],
      dialogOpen: false,
      maximizedToggle: this.$q.platform.is.mobile,
      addressForm: {
        address1: '',
        address2: '',
        city: '',
        postCode: '',
        stateProvinceRegion: '',
        country: ''
      }
    }
  },
  validations: {
    addressForm: {
      address1: { required },
      address2: { required },
      city: { required },
      postCode: { required },
      stateProvinceRegion: { required },
      country: { required }
    }
  },
  watch: {
    'open': function (val) {
      var me = this
      me.dialogOpen = val
      me.contact = userData.getUserData()
      if (me.contact.addresses.length > 0) {
        me.addressForm = me.contact.addresses[0]
        console.log(me.addressForm)
      }
    },
    'model': function (val) {
      var me = this
      console.log('model updated')
      console.log(val)
      var selected = _.find(me.loadedCodes, { code: val })
      console.log(selected)
    },
    'addressForm.postCode': function (val) {
      var me = this
      if (val.length > 3) {
        contactService.getAddressByZip(val).then((list) => {
          var finded = _.find(list, { code: val })
          console.log(finded)
          if (finded) {
            me.addressForm.city = finded.city
            me.addressForm.country = finded.country
            me.addressForm.stateProvinceRegion = finded.state
          } else {
            me.addressForm.city = ''
            me.addressForm.country = ''
            me.addressForm.stateProvinceRegion = ''
          }
        })
      }
    }
  },
  created: function () {
  },
  methods: {
    autohide () {
      console.log('autohide')
    },
    filterFn (val, update, abort) {
      var me = this
      if (val.length < 3) {
        console.log('abort')
        abort()
        return
      }

      update(() => {
        contactService.getAddressByZip(val).then((list) => {
          me.loadedCodes = list
          me.options = []
          _.each(list, l => {
            me.options.push(l.code)
          })
          if (me.options.length === 0) {
            me.options.push(val)
          }
        }, (error) => {
          console.error(error.message)
        })
        console.log('update')
      })
    },
    saveClicked () {
      var me = this
      me.$v.addressForm.$touch()
      if (me.$v.addressForm.$error) {
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
      updatedContact.Addresses = []
      updatedContact.Addresses.push(me.addressForm)
      me.$q.loading.show()
      contactService.contactUpdate(updatedContact).then((response) => {
        if (response !== 'success') {
          me.$q.notify({
            message: response,
            position: 'bottom',
            timeout: 2000,
            color: 'negative'
          })
          return
        }
        contactService.contactGetByCardId(me.contact.id).then((contact) => {
          userData.setUserData(contact)
          me.$q.loading.hide()
          me.dialogOpen = false
        }, (error) => {
          console.error(error.message)
        })
      }, (error) => {
        me.$q.notify({
          message: error.data.error,
          position: 'bottom',
          timeout: 2000,
          color: 'negative'
        })
        console.error(error.message)
      })
    }
  }
}
</script>
