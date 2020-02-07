<template>
 <q-dialog v-model="dialogOpen" :square="true" @hide="$emit('close')"
 persistent
      :maximized="maximizedToggle"
      transition-show="slide-up"
      transition-hide="slide-down">
      <q-resize-observer @resize="onResize" />
     <div class="i-watch-container" >
<<<<<<< HEAD
       <div class="i-watch-close">
        <q-btn icon="close" flat round dense v-close-popup @click="$emit('close')" />
       </div>
       <h2 class="i-watch-container-name">
            {{itemDetail.description}}
        </h2>
        <div class="i-watch-item">
          <div class="i-watch-img">
            <!--<q-img v-if="item"
                :src="getItemImageUrl(item.id)"
                spinner-color="grey-5"
                class="i-watch-grmoroi"
              />-->
            <div class="i-watch-body-line-right category">
              {{itemDetail.itemCategory}}
=======
      <h2 class="i-watch-title">{{itemDetail.description}}</h2>
       <div class="i-watch-close">
       <q-btn icon="close" flat round dense v-close-popup @click="$emit('close')" />
       </div>
        <div class="i-watch-item">
          <div class="i-watch-img">
            <div class="i-watch-body-line">
              <div class="i-watch-body-line-right">
                {{itemDetail.itemCatefory}}
              </div>
>>>>>>> 789d82e4a6be9b2b5504684c65cbbbe964942d54
            </div>
            <img v-if="item" class="t-watch-pic" :src="getItemImageUrl(item.id)"/>
          </div>
          <div class="i-watch-body" v-if="Object.keys(itemDetail).length !== 0">
<<<<<<< HEAD
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
              <div class="i-watch-body-line-right category">
               {{itemDetail.brand}}
              </div>
            </div>
            <div class="i-watch-body-line">
              <div class="i-watch-body-line-right category">
=======
            <div class="i-watch-body-line">
              <div class="i-watch-body-line-right">
               {{itemDetail.no}}
              </div>
            </div>
            <div class="i-watch-body-line">
              <div class="i-watch-body-line-right">
>>>>>>> 789d82e4a6be9b2b5504684c65cbbbe964942d54
               {{itemDetail.productGroup}}
              </div>
            </div>
            <div class="i-watch-body-line">
<<<<<<< HEAD
              <div class="i-watch-body-line-left">
                ALLOWANCE:
              </div>
              <div class="i-watch-body-line-right">
              {{itemDetail.allowance}}
              </div>
            </div>
            <div class="i-watch-body-line">
              <div class="i-watch-body-line-right description">
               {{itemDetail.HTMLDescription}}
              </div>
            </div>
            <div class="i-watch-body-bottom">
              <div class="i-watch-body-bottom-left">
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
              <div class="i-watch-body-bottom-right">
                <div class="i-watch-body-bottom-price">
                  <span>$</span>{{parseFloat(itemDetail.price).toFixed(2).split('.')[0]}}<sup>.{{parseFloat(itemDetail.price).toFixed(2).split('.')[1]}}</sup>
                </div>
                <div class="i-watch-body-bottom-strain">
                  {{itemDetail.strain}}-STRAIN CODE
                </div>
=======
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
            <!--<div class="i-watch-body-line">
              <div class="i-watch-body-line-left">
                Unit Of Measure:
              </div>
              <div class="i-watch-body-line-right">
               {{itemDetail.baseUnitOfMeasure}}
              </div>
            </div>-->
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
>>>>>>> 789d82e4a6be9b2b5504684c65cbbbe964942d54
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

<<<<<<< HEAD
<style scoped>
.i-watch-body-bottom {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}

.i-watch-body-bottom .i-watch-body-bottom-left {
  padding-right: 10px;
}

.i-watch-body-bottom .i-watch-body-line {
  padding-bottom: 0;
  font-family: 'Museo Sans';
  font-size: 14px;
  padding-bottom: 5px;
}

.i-watch-body-bottom-right {
  flex-direction: column;
  display: flex;
  justify-content: space-between;
}

.i-watch-body-bottom-price {
  font-size: 40px;
  font-weight: bold;
  font-family: 'Museo Sans';
  color: #58595B;
  text-align: right;
}

.i-watch-body-bottom-price span {
  font-size: 20px;
  margin-right: 5px;
  vertical-align: bottom;
  font-weight: 500;
}

.i-watch-body-bottom-price sup {
  font-size: 20px;
  font-weight: 500;
  margin-left: 5px;
}

.i-watch-body-bottom-strain {
  font-size: 10px;
  font-family: 'Museo Sans';
  color: #818181;
  text-align: right;
}

=======
<style>
>>>>>>> 789d82e4a6be9b2b5504684c65cbbbe964942d54
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
  position: absolute;
  top: 5px;
  right: 5px;
}
.i-watch-container{
  display: flex;
  flex-direction: column;
  border:10px solid #333333;
  background: white;
  max-width: 800px!important;
<<<<<<< HEAD
  padding: 38px;
  position: relative;
}
h2.i-watch-container-name {
  font-size: 24px;
  font-weight: bold;
  font-family: gotham;
  text-align: center;
  margin: 0;
=======
  padding: 48px;
  position: relative;
}
.i-watch-title{
  margin: 0;
  font-size: 24px;
  font-family: gotham;
  font-weight: bold;
  text-align: center;
  text-transform: uppercase;
>>>>>>> 789d82e4a6be9b2b5504684c65cbbbe964942d54
  line-height: 24px;
  margin-bottom: 48px;
}
.i-watch-item{
  display: flex;
  flex-direction: row;
<<<<<<< HEAD
=======
  padding: 20px;
  padding-top:0px;
>>>>>>> 789d82e4a6be9b2b5504684c65cbbbe964942d54
}
.i-dialog{
  border:10px solid #333333;
}
.i-dialog-section{
  padding-top:1px;
}
.i-watch-img{
<<<<<<< HEAD
  flex: 1;
  flex-direction: column;
  display: flex;
  padding-right: 20px;
}
.i-watch-img img {
  width: 100%;
=======
  width:50%;
  flex-direction: column;
  display: flex;
  align-items: center;
>>>>>>> 789d82e4a6be9b2b5504684c65cbbbe964942d54
}
.i-watch-body{
  display:flex;
  flex-direction: column;
<<<<<<< HEAD
  flex: 1;
=======
  justify-content:center;
>>>>>>> 789d82e4a6be9b2b5504684c65cbbbe964942d54
}
.i-watch-body-line{
  display:flex;
  flex-direction: row;
<<<<<<< HEAD
  padding-bottom:16px;
=======
  padding-bottom:10px;
>>>>>>> 789d82e4a6be9b2b5504684c65cbbbe964942d54
}
.i-watch-body-line-left{
  display: flex;
  flex-direction: row;
<<<<<<< HEAD
  padding-right: 10px;
=======
  width: 100%;
  padding-right: 10px;
  justify-content: flex-end;
>>>>>>> 789d82e4a6be9b2b5504684c65cbbbe964942d54
  color:#818181;
}
.i-watch-body-line-right{
  display: flex;
  flex-direction: row;
<<<<<<< HEAD
  color:#58595B;
  font-weight: bold;
}
.i-watch-body-line-right.category{
  width: 100%;
  text-transform: uppercase;
  color: #818181;
  font-family: 'Museo Sans';
  font-size: 14px;
}
.i-watch-body-line-right.description {
  font-family: 'Museo Sans';
  font-size: 14px;
  padding-bottom: 48px;
}
=======
  width: 80%;
  color:#58595B;
  font-weight: bold;
}
>>>>>>> 789d82e4a6be9b2b5504684c65cbbbe964942d54
.i-watch-body-line-title{
font-weight: bold;
}
  .t-watch-pic{
    max-height: 75vh;
    max-width: 100%;
  }
@media only screen and (max-width: 600px) {
<<<<<<< HEAD
  .i-watch-container {
    padding: 30px;
  }
  .i-watch-body-bottom-price {
    text-align: right;
  }
  .i-watch-close {
    right: 0
  }
=======
>>>>>>> 789d82e4a6be9b2b5504684c65cbbbe964942d54
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
<<<<<<< HEAD
  padding-bottom: 10px;
=======
  padding-bottom:0px;
>>>>>>> 789d82e4a6be9b2b5504684c65cbbbe964942d54
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
