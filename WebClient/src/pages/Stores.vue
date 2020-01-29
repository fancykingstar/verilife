<template>
  <div class="i-history-container">
     <q-pull-to-refresh
      @refresh="refresh"
      color="black"
    >
    <div class="i-history-title">Stores</div>
    <!-- GmapMap ref="mapRef" :center="{lat: 1.38, lng: 103.8}" :zoom="12" class="map-container">
          </GmapMap> -->
    <div>
        <q-tabs
          v-model="tab"
          dense
          class="text-grey"
          active-color="grey-6"
          indicator-color="grey-6"
          align="right"
          narrow-indicator
        >
          <q-tab name="list" label="List" />
         <!-- <q-tab name="receipt" label="Receipt list" /> -->
        </q-tabs>

        <q-tab-panels v-model="tab" animated>
          <q-tab-panel name="list">
              <div class="i-history-transactions"  v-for="(location, idx) in locations" :key="idx">
                <div class="i-history-item">
                    <div class="i-history-body">
                      <div class="i-history-body-name">{{location.storeName}}</div>
                      <div class="i-history-body-price">{{location.postCode}} {{location.county}} {{location.address}} </div>
                      <div class="i-history-body-price">{{location.address2}} </div>
                      <div class="i-history-body-price">  <q-icon name="phone" /> {{location.storePhone}} </div>
                      <div class="i-history-body-price">  <q-icon name="mail" /> {{location.storeEmail}} </div>
                    </div>
                  </div>
            </div>
          </q-tab-panel>

          <q-tab-panel name="receipt">
          <!-- <GmapMap ref="mapRef">
          </GmapMap> -->
          </q-tab-panel>
        </q-tab-panels>

    </div>
    </q-pull-to-refresh>

  </div>
</template>

<style>
.map-container {
    width: 400px;
    height: 400px;
  }
.i-history-body-refund{
  color: #F44336;
    font-family: Din;
    font-weight: bold;
    font-size: 20px;
    line-height: 20px;
}
.i-pop-body img{
  max-width:400px;
  height: auto;
}
.i-pop-body{
    text-align: center;
}
.i-pop-container{
  display: flex;
  flex-direction: column;
  border:10px solid #333333;
  background: white;
  max-width: 800px!important;
}
.i-pop-close{
  display:flex;
  flex-direction: row;
  justify-content: flex-end;
  padding:3px;
  height:40px;
}
.i-pop-body{
  padding:20px;
}
.i-history-trans-get{
  color: #7d7d7d;
  padding-top:5px;
  cursor:pointer;
}
.i-history-trans-date-year{
  font-size:15px;
}
.i-history-trans-date-day{
font-size: 35px;
    line-height: 35px;
}
.i-history-trans-date-month{
font-size:15px;
}
.i-history-trans-date-day-box{
    display: flex;
    flex-direction: row;
}
.i-history-trans-price{
 color:#58595B;
  font-family: Din;
  font-weight: bold;
  font-size: 38px;
  line-height: 38px;
}
.i-history-trans-name{
      color: #7d7d7d;
}
.i-history-trans-box{
      color: #58595B;
  justify-content: flex-start;
    width: 30%;
    display: flex;
}
.i-history-trans-date{
  display:flex;
  width:80%;
  flex-direction: column;
  padding:2px 10px;
  background: #dbdcdb;
}
.i-history-container{
  display: flex;
  flex-direction: column;
  padding:10px;
  padding-left:10%;
  padding-right:10%;
}

.i-history-title{
  text-align: center;
  padding: 10px;
  font-size: 20px;
  color:#58595B;
  font-family: Din;
  font-weight: bold;
  text-transform: uppercase;
}
.i-history-item{
  padding: 10px;
  display: flex;
  flex-direction: row;
  border-top: 1px solid #58595B;
}
.i-history-img{
  width:30%;
  justify-content: center;
    width: 30%;
    display: flex;
}

.i-history-body{
  display:flex;
  flex-direction: column;
  padding-left:10px;
}

.t-h-img{
  max-width: 300px; max-height: 300px;width: auto;
  height: auto;
}
.my-card{
  cursor:pointer;
}
.t-row{
  display: flex;
  flex-direction: row;
}
.i-history-body-date{
  color:#7d7d7d;
}
.i-history-body-name{
  color:#58595B;
  font-family: Din;
  font-weight: bold;
  font-size: 18px;
}
.i-history-body-price{
   color:#7d7d7d;
}

@media only screen and (max-width: 400px) {
  .i-history-container{
    padding-left:0%;
    padding-right:0%;
  }
  .i-history-trans-box{
    width: 40%;
  }

}

@media only screen and (max-width: 430px) {
    .i-pop-body img{
  max-width:300px;
  height: auto;
}
.i-pop-body{
  font-size:10px;
    text-align: center;
}
}

@media only screen and (max-width: 330px) {
    .i-pop-body img{
  max-width:250px;
  height: auto;
}
.i-pop-body{
  font-size:8px;
    text-align: center;
}
}
</style>

<script>
import contactService from '../app/services/contactservice'

export default {
  name: 'Stores',
  components: {
  },
  data () {
    return {
      locations: [],
      tab: 'list'
    }
  },
  mounted () {
    // At this point, the child GmapMap has been mounted, but
    // its map has not been initialized.
    // Therefore we need to write mapRef.$mapPromise.then(() => ...)

    this.$refs.mapRef.$mapPromise.then((map) => {
      map.panTo({
        lat: 1.38,
        lng: 103.80 })
    })
  },
  computed: {
    contentStyle () {
      return {
      }
    },
    contentActiveStyle () {
      return {
      }
    },
    thumbStyle () {
      return {
        right: '2px',
        borderRadius: '5px',
        backgroundColor: '#58595B',
        width: '5px',
        opacity: 0.75
      }
    }
  },
  created: function () {
    var me = this
    contactService.getLocations().then((locations) => {
      me.locations = locations
      console.log(locations)
      /*
       me.$refs.mapRef.$mapPromise.then((map) => {
        map.panTo({
          lat: 1.38,
          lng: 103.80
        })
      })
      */
    })
  },
  methods: {
    refresh (done) {
      done()
    }
  }
}
</script>
