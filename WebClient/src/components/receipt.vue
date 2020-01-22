<template>
 <q-dialog v-model="dialogOpen" :square="true" @hide="$emit('close')"
 persistent
      :maximized="maximizedToggle"
      transition-show="slide-up"
      transition-hide="slide-down">
      <q-resize-observer @resize="onResize" />
     <div class="i-watch-container" >
       <div class="i-watch-close">
       <q-btn icon="close" flat round dense v-close-popup @click="$emit('close')" />
       </div>
        <div class="i-watch-item">
          <div class="i-watch-img">
            <!--<q-img v-if="item"
                :src="getItemImageUrl(item.id)"
                spinner-color="grey-5"
                class="i-watch-grmoroi"
              />-->
            <img v-if="item" class="t-watch-pic" :src="getItemImageUrl(item.id)"/>
          </div>
          <div class="i-watch-body">
            <div class="i-watch-body-line q-mb-sm i-watch-body-line-title">
               {{itemDetail.description}}
            </div>

            <div class="i-watch-body-line">
              <div class="i-watch-body-line-left">
                Gender:
              </div>
              <div class="i-watch-body-line-right">
               {{itemDetail.gender}}
              </div>
            </div>

                <div class="i-watch-body-line">
              <div class="i-watch-body-line-left">
                Movement Vendor:
              </div>
              <div class="i-watch-body-line-right">
               {{itemDetail.movementVendor}}
              </div>
            </div>

            <div class="i-watch-body-line">
              <div class="i-watch-body-line-left">
                Movement Caliber:
              </div>
              <div class="i-watch-body-line-right">
               {{itemDetail.movementCaliber}}
              </div>
            </div>
            <div class="i-watch-body-line">
              <div class="i-watch-body-line-left">
               Movement Comp:
              </div>
              <div class="i-watch-body-line-right">
               {{itemDetail.movementComponents}}
              </div>
            </div>

               <div class="i-watch-body-line">
              <div class="i-watch-body-line-left">
               Movement Assembly:
              </div>
              <div class="i-watch-body-line-right">
              {{itemDetail.movementAssembly}}
              </div>
            </div>

            <div class="i-watch-body-line">
              <div class="i-watch-body-line-left">
              Function Type:
              </div>
              <div class="i-watch-body-line-right">
               {{itemDetail.functionType}}
              </div>
            </div>

               <div class="i-watch-body-line">
              <div class="i-watch-body-line-left">
               Case Material:
              </div>
              <div class="i-watch-body-line-right">
                  {{itemDetail.caseMaterial}}
              </div>
            </div>
          </div>
      </div>
    </div>
        <!--<q-card class="i-dialog" v-if="item">
        <q-card-section class="row items-center i-dialog-section">
          <div class="text-h6"></div>
          <q-space />
          <q-btn icon="close" flat round dense v-close-popup @click="$emit('close')"/>
        </q-card-section>

        <q-card-section>
         div class="q-px-lg q-pb-md">
             {{no}}
            <qrcode :value="contact.id" :options="{ width: 300 }"></qrcode>
          </div>
        </q-card-section>
      </q-card>-->
    </q-dialog>

</template>

<style>
.i-dialog{
  border:10px solid #333333;
}
.i-dialog-section{
  padding-top:1px;
}
</style>

<script>
import contactService from '../app/services/contactservice'
import userData from '../app/services/userdata'

export default {
  name: 'Watch',
  components: {
  },
  props: ['storeNo', 'posTerminalNo', 'transactionNo'],
  data () {
    return {
      dialogOpen: false,
      maximizedToggle: this.$q.platform.is.mobile
    }
  },
  watch: {
    'open': function (val) {
      console.log('open: ' + open)
      var me = this
      if (val === true) {
        me.$q.loading.show()
        contactService.getItemDetails(me.item.id).then((response) => {
          console.log('item detail: ')
          console.log(response)
          me.itemDetail = response
          me.dialogOpen = val
        }, (error) => {
          console.error(error.message)
          me.dialogOpen = false
        }).then(() => {
          me.$q.loading.hide()
        })
      } else {
        me.dialogOpen = false
      }
      console.log(me.item)
    }
  },
  created: function () {
    var me = this
    var contact = userData.getUserData()
    if (contact) {
      me.contact = contact
    }
  },
  methods: {
    onResize (size) {
      var me = this
      if (size.width < 400) {
        me.isMobile = true
      } else {
        me.isMobile = false
      }
    },
    getItemImageUrl (id) {
      return process.env.API + '/images/item/' + id
    }
  }
}
</script>
