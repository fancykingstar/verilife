<template>
        <div class="">

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
        <span class="i-change-input-title" style="padding-top:0px">Zip Code</span>

          <div style="display:flex; flex-direction:row;width:100%">
            <div style="display:flex; width:100%">
               <q-input outlined stack-label square color="grey" style="width:100%;margin-right:5px;"
                  bg-color="white" :error="$v.addressForm.postCode.$error"
                v-model="addressForm.postCode" class=""/>
            </div>
            <div style="display:flex; width:30%">
             <div class="i-lookup" @click="zipLookup"> LOOKUP </div>
            </div>
          </div>

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

        <div @click="saveClicked" class="i-btn-contact">SAVE ADDRESS</div>

        <q-dialog v-model="lookUpDialogError">
            <q-card>
              <q-card-section class="row items-center">
                <span class="q-ml-sm">No addresses were found matching that zip code</span>
              </q-card-section>

              <q-card-actions align="right">
                <q-btn flat label="OK" color="grey" v-close-popup />
              </q-card-actions>
            </q-card>
          </q-dialog>

        </div>

</template>

<style>
.q-field--auto-height .q-field__control, .q-field--auto-height .q-field__native {
    min-height: 40px;
}
.i-lookup{
  display: flex;
  flex-direction: column;
  justify-content: center;
  background-color: #C7C8CA;
  padding: 3px 10px;
  color: #58595B;
  font-weight: bold;
  cursor: pointer;
  user-select: none;
}
</style>

<script>
import { required, maxLength } from 'vuelidate/lib/validators'
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
      lookUpDialogError: false,
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
      address1: { required, maxLength: maxLength(80) },
      city: { required, maxLength: maxLength(60) },
      postCode: { required, maxLength: maxLength(10) },
      stateProvinceRegion: { required, maxLength: maxLength(30) },
      country: { required, maxLength: maxLength(30) }
    }
  },
  watch: {
    /*
    'model': function (val) {
      var me = this
      console.log('model updated')
      console.log(val)
      var selected = _.find(me.loadedCodes, { code: val })
      console.log(selected)
    },
    */
  },
  created: function () {
    var me = this
    me.contact = userData.getUserData()
    if (me.contact.addresses.length > 0) {
      me.addressForm = me.contact.addresses[0]
      console.log(me.addressForm)
    }
  },
  methods: {
    zipLookup () {
      var me = this
      if (me.addressForm.postCode.length !== 5) {
        me.lookUpDialogError = true
        return
      }
      contactService.getAddressByZip(me.addressForm.postCode).then((list) => {
        var finded = _.find(list, { code: me.addressForm.postCode })
        if (finded) {
          me.addressForm.city = finded.city
          me.addressForm.country = finded.country
          me.addressForm.stateProvinceRegion = finded.state
        } else {
          me.lookUpDialogError = true
        }
      })
    },
    /*
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
    */
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
