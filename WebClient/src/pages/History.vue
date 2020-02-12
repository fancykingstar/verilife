<template>
  <div class="i-history-container">
     <q-pull-to-refresh
      @refresh="refresh"
      color="black"
    >
    <watch :item="selectedWatch" :open="watchDialog" @close="watchDialog = false"/>
    <div class="i-history-title">payment history</div>
    <div v-if="contact != null">
        <q-tabs
          v-model="tab"
          dense
          class="text-grey"
          active-color="grey-6"
          indicator-color="grey-6"
          align="right"
          narrow-indicator
        >
          <q-tab name="watch" label="Items list" />
          <q-tab name="receipt" label="Receipt list" />
        </q-tabs>

        <q-tab-panels v-model="tab" animated>
          <q-tab-panel name="watch">
              <div class="i-history-transactions"  v-for="(transaction, idx) in contact.transactions" :key="idx">
                <div v-for="(sale, idx2) in transaction.saleLines" :key="idx2" >
                  <div class="i-history-item watch">
                    <div class="i-history-img">
                      <q-img @click="openWatch(sale.item)"
                          :src="getItemImageUrl(sale.item.id)"
                          :ratio="1"
                          spinner-color="grey-5"
                        />
                      <!-- <img class="t-h-img" :src="getItemImageUrl(sale.item.id)"/> -->
                    </div>
                    <div class="i-history-body">
                      <div class="i-history-body-date">{{transaction.date}}</div>
                      <div class="i-history-body-name">{{sale.item.description}}</div>
                      <div class="i-history-body-refund" v-if="sale.refund">REFUNDED</div>
                    </div>
                    <div class="i-history-body-price"> $ {{parseFloat(thousandsSeparators(sale.netAmt + sale.discountAmt)).toFixed(2)}}</div>
                  </div>
              </div>
            </div>
          </q-tab-panel>

          <q-tab-panel name="receipt">

              <div class="i-history-item"  v-for="(transaction, idx) in contact.transactions" :key="idx">
                <div class="i-history-trans-box">
                  <div class="i-history-trans-date">
                    <div class="i-history-trans-date-month">
                      {{transaction.month}}
                    </div>
                    <div class="i-history-trans-date-day-box">
                      <div class="i-history-trans-date-day">{{transaction.day}}</div>
                      <div class="i-history-trans-date-dayend">  {{transaction.dayEnd}}</div>
                    </div>
                    <div class="i-history-trans-date-year">
                      {{transaction.year}}
                    </div>
                  </div>
                  <!-- <img class="t-h-img" :src="getItemImageUrl(sale.item.id)"/> -->
                </div>
                <div class="i-history-body">
                  <div class="i-history-trans-name">{{transaction.receiptNumber}}</div>
                  <div class="" v-if="transaction.refund">
                    <span class="i-history-body-refund">REFUNDED</span>
                  </div>
                  <div class="i-history-trans-price">$ {{parseFloat(thousandsSeparators(transaction.netAmt)).toFixed(2)}}</div>
                  <div @click="getReceiptJSON(transaction)" class="i-history-trans-get"><q-icon name="visibility" /> VIEW</div>
                  <a :href="getReceiptUrl(transaction)" target="_blank" style="text-decoration:none">
                  <div class="i-history-trans-get"><q-icon name="get_app" /> DOWNLOAD</div>
                  </a>
                </div>
            </div>
          </q-tab-panel>
        </q-tab-panels>
         <q-dialog v-model="receiptDialog" :maximized="maximizedToggle"
              transition-show="slide-up"
              transition-hide="slide-down">
              <div class="i-pop-container" >
              <div class="i-pop-close" @click="receiptDialog = false">
              <q-btn icon="close" flat round dense v-close-popup />
              </div>
               <q-scroll-area style="height: calc(100vh - 100px);margin-top:30px;"  :thumb-style="thumbStyle"
              :content-style="contentStyle"
              :content-active-style="contentActiveStyle">
                <div class="i-pop-body">
                  <img src="statics/veri-logo-black.png">
                  <div style="font-family:Merch" v-for="(line, idx3) in receiptLines" :key="'r-' + idx3">
                    <barcode width="1" height="80" fontSize="14" v-if="idx3 === receiptLines.length-2" v-bind:value="line.lineText">
                      Show this if the rendering fails.
                    </barcode>
                    <span v-if="line.fontType === 0 && idx3 !== receiptLines.length-2" v-html="line.lineText"></span>
                    <span style="font-weight:bold" v-if="line.fontType !== 0" v-html="line.lineText"></span>
                  </div>
                  <!--<img src="statics/receipt-footer.png">-->
                </div>
                  </q-scroll-area>
                </div>
            </q-dialog>
    </div>
    </q-pull-to-refresh>

  </div>
</template>

<style>
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
  justify-content: center;
  width: 136px;
  height: 136px;
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
  color:#cccccc;
  font-family: Museo Sans;
}
.i-history-body-name{
  color:#58595B;
  font-family: Gotham;
  font-weight: bold;
  font-size: 18px;
}
.i-history-body-price {
  color:#7d7d7d;
  font-family: Museo;
  font-size: 24px;
  flex: 1;
  text-align: right;
}

.i-history-body .i-history-body-price {
  font-family: Din;
  font-size: 14px;
  text-align: left;
}

@media only screen and (max-width: 767px) {
  .i-history-item.watch .i-history-body {
    flex: 1;
  }
}

@media only screen and (max-width: 520px) {
  .i-history-item.watch {
    flex-direction: column;
    align-items: center;
  }

  .i-history-item.watch .img {
    width: 50%
  }

  .i-history-item.watch .i-history-body {
    align-items: center;
    text-align: center;
    margin-bottom: 20px;
  }
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
import _ from 'lodash'
import moment from 'moment'
import watch from '../components/watch'
import contactService from '../app/services/contactservice'
import userData from '../app/services/userdata'
import VueBarcode from 'vue-barcode'

export default {
  name: 'History',
  components: {
    watch: watch,
    'barcode': VueBarcode
  },
  data () {
    return {
      contact: null,
      tab: 'watch',
      selectedWatch: null,
      watchDialog: false,
      receiptLines: [],
      receiptDialog: false,
      maximizedToggle: true,
      barcodeValue: 'T00000P0003000000003'
    }
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
    var contact = userData.getUserData()
    if (contact) {
      me.getRefundList(contact, function (filledContact) {
        me.contact = filledContact
      })
    }
  },
  methods: {
    refresh (done) {
      var me = this
      var contact = userData.getUserData()
      if (contact) {
        contactService.contactGetByCardId(contact.account.id).then((rcontact) => {
          me.getRefundList(rcontact, function (filledContact) {
            me.contact = filledContact
            done()
          })
        })
      }
    },
    thousandsSeparators (num) {
      num = Math.round(num * 100) / 100
      var numParts = num.toString().split('.')
      numParts[0] = numParts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',')
      return numParts.join('.')
    },
    getRefundList (contact, callback) {
      contactService.getRefundList(contact.id).then((refund) => {
        contact.transactions = _.reverse(_.orderBy(contact.transactions, 'date'))
        _.each(contact.transactions, (t) => {
          t.refund = false
          t.month = moment(t.date).format('MMMM')
          t.day = moment(t.date).format('DD')
          t.dayEnd = moment(t.date).format('Do').replace(/[0-9]/g, '')
          t.year = moment(t.date).format('YYYY')
          t.date = moment(t.date).format('MMMM Do YYYY')
          _.each(t.saleLines, (sale) => {
            sale.refund = false
          })
        })
        _.each(contact.transactions, (t) => {
          var isRefTrans = _.find(refund, { documentNo: t.receiptNumber })
          if (isRefTrans) {
            t.refund = true
          }
          _.each(t.saleLines, (sale) => {
            var isRefItem = _.find(refund, { itemNo: sale.item.id, documentNo: t.receiptNumber })
            if (isRefItem) {
              sale.refund = true
            }
          })
        })
        callback(contact)
      })
    },
    getReceiptJSON (transaction) {
      var me = this
      me.$q.loading.show()
      var store = transaction.tenderLines[0].storeId
      contactService.getDigitalReceiptJSON(store, transaction.terminal, transaction.id, me.contact.externalInfo.companyName).then((receipt) => {
        console.log(receipt.printLines, '999999999999')
        _.each(receipt.printLines, (line) => {
          line.lineText = line.lineText.replace(/ /g, '&nbsp;')
        })
        me.receiptLines = receipt.printLines
        me.receiptDialog = true
      }).then(() => {
        me.$q.loading.hide()
      })
    },
    openWatch (item) {
      var me = this
      // console.log(id)
      me.selectedWatch = item
      me.watchDialog = true
    },
    getItemImageUrl (id) {
      return process.env.API + '/images/item/' + id
    },
    getReceiptUrl (transaction) {
      var me = this
      console.log(transaction)
      var store = transaction.tenderLines[0].storeId
      var ret = process.env.API + '/contact/GetDigitalReceipt?StoreNo=' + store + '&PosTerminalNo=' + transaction.terminal + '&TransactionNo=' + transaction.id + '&Company=' + me.contact.externalInfo.companyName
      return ret
      // return process.env.API + '/images/item/' + id
    }
  }
}
</script>
