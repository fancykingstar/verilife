(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["237ba8d0"],{"10af":function(t,i,e){},"143e":function(t,i,e){},"2b3b":function(t,i,e){"use strict";var a=function(){var t=this,i=t.$createElement,e=t._self._c||i;return e("q-dialog",{attrs:{square:!0,persistent:"",maximized:t.maximizedToggle,"transition-show":"slide-up","transition-hide":"slide-down"},on:{hide:function(i){return t.$emit("close")}},model:{value:t.dialogOpen,callback:function(i){t.dialogOpen=i},expression:"dialogOpen"}},[e("q-resize-observer",{on:{resize:t.onResize}}),e("div",{staticClass:"i-watch-container"},[e("div",{staticClass:"i-watch-close"},[e("q-btn",{directives:[{name:"close-popup",rawName:"v-close-popup"}],attrs:{icon:"close",flat:"",round:"",dense:""},on:{click:function(i){return t.$emit("close")}}})],1),e("h2",{staticClass:"i-watch-container-name"},[t._v("\n            "+t._s(t.itemDetail.description)+"\n        ")]),e("div",{staticClass:"i-watch-item"},[e("div",{staticClass:"i-watch-img"},[e("div",{staticClass:"i-watch-body-line-right category"},[t._v("\n              "+t._s(t.itemDetail.itemCategory)+"\n            ")]),t.item?e("img",{staticClass:"t-watch-pic",attrs:{src:t.getItemImageUrl(t.item.id)}}):t._e()]),0!==Object.keys(t.itemDetail).length?e("div",{staticClass:"i-watch-body"},[e("div",{staticClass:"i-watch-body-line"},[e("div",{staticClass:"i-watch-body-line-right category"},[t._v("\n               "+t._s(t.itemDetail.brand)+"\n              ")])]),e("div",{staticClass:"i-watch-body-line"},[e("div",{staticClass:"i-watch-body-line-right category"},[t._v("\n               "+t._s(t.itemDetail.productGroup)+"\n              ")])]),e("div",{staticClass:"i-watch-body-line"},[e("div",{staticClass:"i-watch-body-line-left"},[t._v("\n                ALLOWANCE:\n              ")]),e("div",{staticClass:"i-watch-body-line-right"},[t._v("\n              "+t._s(t.itemDetail.allowance)+"\n              ")])]),e("div",{staticClass:"i-watch-body-line"},[e("div",{staticClass:"i-watch-body-line-right description"},[t._v("\n               "+t._s(t.itemDetail.HTMLDescription)+"\n              ")])]),e("div",{staticClass:"i-watch-body-bottom"},[e("div",{staticClass:"i-watch-body-bottom-left"},[e("div",{staticClass:"i-watch-body-line"},[e("div",{staticClass:"i-watch-body-line-left"},[t._v("\n                    THC:\n                  ")]),e("div",{staticClass:"i-watch-body-line-right"},[t._v("\n                  "+t._s(t.itemDetail.THC)+"\n                  ")])]),e("div",{staticClass:"i-watch-body-line"},[e("div",{staticClass:"i-watch-body-line-left"},[t._v("\n                    CBD:\n                  ")]),e("div",{staticClass:"i-watch-body-line-right"},[t._v("\n                  "+t._s(t.itemDetail.CBD)+"\n                  ")])]),e("div",{staticClass:"i-watch-body-line"},[e("div",{staticClass:"i-watch-body-line-left"},[t._v("\n                    CBN:\n                  ")]),e("div",{staticClass:"i-watch-body-line-right"},[t._v("\n                  "+t._s(t.itemDetail.CBN)+"\n                  ")])])]),e("div",{staticClass:"i-watch-body-bottom-right"},[e("div",{staticClass:"i-watch-body-bottom-price"},[e("span",[t._v("$")]),t._v(t._s(parseFloat(t.itemDetail.price).toFixed(2).split(".")[0])),e("sup",[t._v("."+t._s(parseFloat(t.itemDetail.price).toFixed(2).split(".")[1]))])]),e("div",{staticClass:"i-watch-body-bottom-strain"},[t._v("\n                  "+t._s(t.itemDetail.strain)+"-STRAIN CODE\n                ")])])])]):t._e()])])],1)},n=[],s=(e("6b54"),e("06db"),e("4c23")),o=e("43c2"),c={name:"Watch",components:{},props:["item","open"],data:function(){return{isMobile:!1,contact:{id:""},itemDetail:{},dialogOpen:!1,maximizedToggle:this.$q.platform.is.mobile}},watch:{open:function(t){function i(i){return t.apply(this,arguments)}return i.toString=function(){return t.toString()},i}(function(t){console.log("open: "+open);var i=this;!0===t?(i.$q.loading.show(),s["a"].getItemDetails(i.item.id).then(function(e){console.log("item detail: "),console.log(e),i.itemDetail=e,i.dialogOpen=t},function(t){console.error(t.message),i.dialogOpen=!1}).then(function(){i.$q.loading.hide()})):i.dialogOpen=!1,console.log(i.item)})},created:function(){var t=this,i=o["a"].getUserData();i&&(t.contact=i)},methods:{onResize:function(t){var i=this;t.width<400?i.isMobile=!0:i.isMobile=!1},getItemImageUrl:function(t){return"https://verilifeapi.tenging.is/api/images/item/"+t}}},r=c,l=(e("647e"),e("2877")),d=Object(l["a"])(r,a,n,!1,null,"31f3b154",null);i["a"]=d.exports},"647e":function(t,i,e){"use strict";var a=e("143e"),n=e.n(a);n.a},"7eff":function(t,i,e){"use strict";var a=e("10af"),n=e.n(a);n.a},f3e7:function(t,i,e){"use strict";e.r(i);var a=function(){var t=this,i=t.$createElement,e=t._self._c||i;return e("div",{staticClass:"i-history-container"},[e("q-pull-to-refresh",{attrs:{color:"black"},on:{refresh:t.refresh}},[e("watch",{attrs:{item:t.selectedWatch,open:t.watchDialog},on:{close:function(i){t.watchDialog=!1}}}),e("div",{staticClass:"i-history-title"},[t._v("payment history")]),null!=t.contact?e("div",[e("q-tabs",{staticClass:"text-grey",attrs:{dense:"","active-color":"grey-6","indicator-color":"grey-6",align:"right","narrow-indicator":""},model:{value:t.tab,callback:function(i){t.tab=i},expression:"tab"}},[e("q-tab",{attrs:{name:"watch",label:"Items list"}}),e("q-tab",{attrs:{name:"receipt",label:"Receipt list"}})],1),e("q-tab-panels",{attrs:{animated:""},model:{value:t.tab,callback:function(i){t.tab=i},expression:"tab"}},[e("q-tab-panel",{attrs:{name:"watch"}},t._l(t.contact.transactions,function(i,a){return e("div",{key:a,staticClass:"i-history-transactions"},t._l(i.saleLines,function(a,n){return e("div",{key:n},[e("div",{staticClass:"i-history-item"},[e("div",{staticClass:"i-history-img"},[e("q-img",{attrs:{src:t.getItemImageUrl(a.item.id),ratio:1,"spinner-color":"grey-5"},on:{click:function(i){return t.openWatch(a.item)}}})],1),e("div",{staticClass:"i-history-body"},[e("div",{staticClass:"i-history-body-date"},[t._v(t._s(i.date))]),e("div",{staticClass:"i-history-body-name"},[t._v(t._s(a.item.description))]),a.refund?e("div",{staticClass:"i-history-body-refund"},[t._v("REFUNDED")]):t._e()]),e("div",{staticClass:"i-history-body-price"},[t._v(" $ "+t._s(parseFloat(t.thousandsSeparators(a.netAmt+a.discountAmt)).toFixed(2)))])])])}),0)}),0),e("q-tab-panel",{attrs:{name:"receipt"}},t._l(t.contact.transactions,function(i,a){return e("div",{key:a,staticClass:"i-history-item"},[e("div",{staticClass:"i-history-trans-box"},[e("div",{staticClass:"i-history-trans-date"},[e("div",{staticClass:"i-history-trans-date-month"},[t._v("\n                    "+t._s(i.month)+"\n                  ")]),e("div",{staticClass:"i-history-trans-date-day-box"},[e("div",{staticClass:"i-history-trans-date-day"},[t._v(t._s(i.day))]),e("div",{staticClass:"i-history-trans-date-dayend"},[t._v("  "+t._s(i.dayEnd))])]),e("div",{staticClass:"i-history-trans-date-year"},[t._v("\n                    "+t._s(i.year)+"\n                  ")])])]),e("div",{staticClass:"i-history-body"},[e("div",{staticClass:"i-history-trans-name"},[t._v(t._s(i.receiptNumber))]),i.refund?e("div",{},[e("span",{staticClass:"i-history-body-refund"},[t._v("REFUNDED")])]):t._e(),e("div",{staticClass:"i-history-trans-price"},[t._v("$ "+t._s(parseFloat(t.thousandsSeparators(i.netAmt)).toFixed(2)))]),e("div",{staticClass:"i-history-trans-get",on:{click:function(e){return t.getReceiptJSON(i)}}},[e("q-icon",{attrs:{name:"visibility"}}),t._v(" VIEW")],1),e("a",{staticStyle:{"text-decoration":"none"},attrs:{href:t.getReceiptUrl(i),target:"_blank"}},[e("div",{staticClass:"i-history-trans-get"},[e("q-icon",{attrs:{name:"get_app"}}),t._v(" DOWNLOAD")],1)])])])}),0)],1),e("q-dialog",{attrs:{maximized:t.maximizedToggle,"transition-show":"slide-up","transition-hide":"slide-down"},model:{value:t.receiptDialog,callback:function(i){t.receiptDialog=i},expression:"receiptDialog"}},[e("div",{staticClass:"i-pop-container"},[e("div",{staticClass:"i-pop-close",on:{click:function(i){t.receiptDialog=!1}}},[e("q-btn",{directives:[{name:"close-popup",rawName:"v-close-popup"}],attrs:{icon:"close",flat:"",round:"",dense:""}})],1),e("q-scroll-area",{staticStyle:{height:"calc(100vh - 100px)","margin-top":"30px"},attrs:{"thumb-style":t.thumbStyle,"content-style":t.contentStyle,"content-active-style":t.contentActiveStyle}},[e("div",{staticClass:"i-pop-body"},[e("img",{attrs:{src:"statics/veri-logo-black.png"}}),t._l(t.receiptLines,function(i,a){return e("div",{key:"r-"+a,staticStyle:{"font-family":"Merch"}},[0===i.fontType?e("span",{domProps:{innerHTML:t._s(i.lineText)}}):t._e(),0!==i.fontType?e("span",{staticStyle:{"font-weight":"bold"},domProps:{innerHTML:t._s(i.lineText)}}):t._e()])})],2)])],1)])],1):t._e()],1)],1)},n=[],s=(e("7514"),e("a481"),e("6b54"),e("06db"),e("28a5"),e("2ef0")),o=e.n(s),c=e("c1df"),r=e.n(c),l=e("2b3b"),d=e("4c23"),h=e("43c2"),p={name:"History",components:{watch:l["a"]},data:function(){return{contact:null,tab:"watch",selectedWatch:null,watchDialog:!1,receiptLines:[],receiptDialog:!1,maximizedToggle:!0}},computed:{contentStyle:function(){return{}},contentActiveStyle:function(){return{}},thumbStyle:function(){return{right:"2px",borderRadius:"5px",backgroundColor:"#58595B",width:"5px",opacity:.75}}},created:function(){var t=this,i=h["a"].getUserData();i&&t.getRefundList(i,function(i){t.contact=i})},methods:{refresh:function(t){var i=this,e=h["a"].getUserData();e&&d["a"].contactGetByCardId(e.account.id).then(function(e){i.getRefundList(e,function(e){i.contact=e,t()})})},thousandsSeparators:function(t){t=Math.round(100*t)/100;var i=t.toString().split(".");return i[0]=i[0].replace(/\B(?=(\d{3})+(?!\d))/g,","),i.join(".")},getRefundList:function(t,i){d["a"].getRefundList(t.id).then(function(e){t.transactions=o.a.reverse(o.a.orderBy(t.transactions,"date")),o.a.each(t.transactions,function(t){t.refund=!1,t.month=r()(t.date).format("MMMM"),t.day=r()(t.date).format("DD"),t.dayEnd=r()(t.date).format("Do").replace(/[0-9]/g,""),t.year=r()(t.date).format("YYYY"),t.date=r()(t.date).format("MMMM Do YYYY"),o.a.each(t.saleLines,function(t){t.refund=!1})}),o.a.each(t.transactions,function(t){var i=o.a.find(e,{documentNo:t.receiptNumber});i&&(t.refund=!0),o.a.each(t.saleLines,function(i){var a=o.a.find(e,{itemNo:i.item.id,documentNo:t.receiptNumber});a&&(i.refund=!0)})}),i(t)})},getReceiptJSON:function(t){var i=this;i.$q.loading.show();var e=t.tenderLines[0].storeId;d["a"].getDigitalReceiptJSON(e,t.terminal,t.id,i.contact.externalInfo.companyName).then(function(t){console.log(t),o.a.each(t.printLines,function(t){t.lineText=t.lineText.replace(/ /g,"&nbsp;")}),i.receiptLines=t.printLines,i.receiptDialog=!0}).then(function(){i.$q.loading.hide()})},openWatch:function(t){var i=this;i.selectedWatch=t,i.watchDialog=!0},getItemImageUrl:function(t){return"https://verilifeapi.tenging.is/api/images/item/"+t},getReceiptUrl:function(t){var i=this;console.log(t);var e=t.tenderLines[0].storeId,a="https://verilifeapi.tenging.is/api/contact/GetDigitalReceipt?StoreNo="+e+"&PosTerminalNo="+t.terminal+"&TransactionNo="+t.id+"&Company="+i.contact.externalInfo.companyName;return a}}},u=p,m=(e("7eff"),e("2877")),v=Object(m["a"])(u,a,n,!1,null,null,null);i["default"]=v.exports}}]);