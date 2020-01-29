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
          <div class="i-watch-body" v-if="Object.keys(itemDetail).length !== 0">
            <div class="i-watch-body-line q-mb-sm i-watch-body-line-title">
               {{itemDetail.description}}
            </div>
<!--
    JObject.Add(JProperty.JProperty('description', StageItemAttributes."Extended Description"));
    JObject.Add(JProperty.JProperty('gender', StageItemAttributes.Gender));
    JObject.Add(JProperty.JProperty('caseSize', StageItemAttributes."Case Size (mm)"));
    JObject.Add(JProperty.JProperty('caseMaterial', StageItemAttributes."Case Material"));
    JObject.Add(JProperty.JProperty('crystalType', StageItemAttributes."Crystal Type"));
    JObject.Add(JProperty.JProperty('movementCaliber', StageItemAttributes."Movement Caliber"));
    JObject.Add(JProperty.JProperty('functionType', StageItemAttributes."Function Type"));
    JObject.Add(JProperty.JProperty('dialMaterial', StageItemAttributes."Dial Material"));
    JObject.Add(JProperty.JProperty('bezelMaterial', StageItemAttributes."Bezel Material"));
    JObject.Add(JProperty.JProperty('crownType', StageItemAttributes."Crown Type"));
    JObject.Add(JProperty.JProperty('brand', StageItemAttributes.Brand));
{
  "no": "1911000001",
  "description": "Flower - Alien Jack - 3.5g",
  "GTIN": "",
  "strainCode": "ALIEN JACK",
  "itemCatefory": "Finished Premium Flower",
  "productGroup": "Dry Flower",
  "baseUnitOfMeasure": "EA",
  "allowance": 0.0,
  "THC": 0.0,
  "CBD": 0.0,
  "CBN": 0.0
}
  -->
            <div class="i-watch-body-line">
              <div class="i-watch-body-line-left">
                No:
              </div>
              <div class="i-watch-body-line-right">
               {{itemDetail.no}}
              </div>
            </div>
            <div class="i-watch-body-line">
              <div class="i-watch-body-line-left">
                Description
              </div>
              <div class="i-watch-body-line-right">
               {{itemDetail.description}}
              </div>
            </div>
            <div class="i-watch-body-line">
              <div class="i-watch-body-line-left">
                Strain Code:
              </div>
              <div class="i-watch-body-line-right">
               {{itemDetail.strainCode}}
              </div>
            </div>
            <div class="i-watch-body-line">
              <div class="i-watch-body-line-left">
                Category
              </div>
              <div class="i-watch-body-line-right">
               {{itemDetail.itemCatefory}}
              </div>
            </div>
            <div class="i-watch-body-line">
              <div class="i-watch-body-line-left">
                Product Group:
              </div>
              <div class="i-watch-body-line-right">
               {{itemDetail.productGroup}}
              </div>
            </div>
            <div class="i-watch-body-line">
              <div class="i-watch-body-line-left">
                Unit Of Measure:
              </div>
              <div class="i-watch-body-line-right">
               {{itemDetail.baseUnitOfMeasure}}
              </div>
            </div>
            <div class="i-watch-body-line">
              <div class="i-watch-body-line-left">
                THC:
              </div>
              <div class="i-watch-body-line-right">
               {{itemDetail.THC}}
              </div>
            </div>
             <div class="i-watch-body-line">
              <div class="i-watch-body-line-left">
                CBD:
              </div>
              <div class="i-watch-body-line-right">
               {{itemDetail.CBD}}
              </div>
            </div>
            <div class="i-watch-body-line">
              <div class="i-watch-body-line-left">
                CBN:
              </div>
              <div class="i-watch-body-line-right">
               {{itemDetail.CBN}}
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
.i-watch-pic{
max-width: 300px;
    max-height: 300px;
    width: auto;
    height: auto;
}
.i-watch-close{
  display:flex;
  flex-direction: row;
  justify-content: flex-end;
  padding:3px;
}
.i-watch-container{
  display: flex;
  flex-direction: column;
  border:10px solid #333333;
  background: white;
  max-width: 800px!important;
}
.i-watch-item{
  display: flex;
  flex-direction: row;
  padding: 20px;
  padding-top:0px;
}
.i-dialog{
  border:10px solid #333333;
}
.i-dialog-section{
  padding-top:1px;
}
.i-watch-img{
  width:50%;
  justify-content: center;
  display: flex;
  align-items: center;
}
.i-watch-body{
  display:flex;
  flex-direction: column;
  justify-content:center;
}
.i-watch-body-line{
  display:flex;
  flex-direction: row;
  padding-bottom:10px;
}
.i-watch-body-line-left{
  display: flex;
  flex-direction: row;
  width: 100%;
  padding-right: 10px;
  justify-content: flex-end;
  color:#818181;
}
.i-watch-body-line-right{
  display: flex;
  flex-direction: row;
  width: 80%;
  color:#58595B;
  font-weight: bold;
}
.i-watch-body-line-title{
font-weight: bold;
}
  .t-watch-pic{
    max-height: 75vh;
    max-width: 100%;
  }
@media only screen and (max-width: 600px) {
 .i-watch-item{
   flex-direction:column;
 }
 .i-watch-img{
   width: 100%;
 }
 .i-watch-body-line-left{
   justify-content: end;
 }
 .i-watch-body{
   padding-top: 20px;
   font-size:12px;
 }
 .i-watch-body-line{
  padding-bottom:0px;
}
.i-watch-container{
   max-width: 100%!important;
}
}
@media only screen and (max-width: 420px) {
  .t-watch-pic{
    max-height: 45vh;
    max-width: 100%;
  }
}
</style>

<script>
import contactService from '../app/services/contactservice'
import userData from '../app/services/userdata'

export default {
  name: 'Watch',
  components: {
  },
  props: ['item', 'open'],
  data () {
    return {
      isMobile: false,
      contact: {
        id: ''
      },
      itemDetail: {

      },
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
