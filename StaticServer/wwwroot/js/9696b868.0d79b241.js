(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["9696b868"],{"0b33":function(t,e,a){"use strict";a.r(e);var n=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("div",{staticClass:"i-history-container stores"},[a("q-pull-to-refresh",{attrs:{color:"black"},on:{refresh:t.refresh}},[a("div",{staticClass:"i-history-title"},[t._v("Locations")]),a("div",[a("q-tabs",{staticClass:"text-grey",attrs:{dense:"","active-color":"grey-6","indicator-color":"grey-6",align:"right","narrow-indicator":""},model:{value:t.tab,callback:function(e){t.tab=e},expression:"tab"}},[a("q-tab",{attrs:{name:"list",label:"List"}}),a("q-tab",{attrs:{name:"map",label:"Map"}})],1),a("q-tab-panels",{attrs:{animated:""},model:{value:t.tab,callback:function(e){t.tab=e},expression:"tab"}},[a("q-tab-panel",{attrs:{name:"map"}},[a("GmapMap",{ref:"mapRef",staticClass:"map-container",staticStyle:{width:"100%"},attrs:{center:{lat:1.38,lng:103.8},zoom:2}},t._l(t.locations,function(e,n){return a("GmapMarker",{key:n,attrs:{position:{lat:e.latitude,lng:e.longitude},clickable:!0,draggable:!0},on:{click:function(a){t.center={lat:e.latitude,lng:e.longitude}}}})}),1)],1),a("q-tab-panel",{attrs:{name:"list"}},t._l(t.locations,function(e,n){return a("div",{key:n,staticClass:"i-history-transactions"},[a("div",{staticClass:"i-history-item"},[a("div",{staticClass:"i-history-body"},[a("div",{staticClass:"i-history-body-name"},[t._v(t._s(e.storeName))]),a("div",{staticClass:"store-list"},[a("div",[a("div",{staticClass:"i-history-body-price"},[t._v(t._s(e.postCode)+" "+t._s(e.county)+" "+t._s(e.address)+" ")]),a("div",{staticClass:"i-history-body-price"},[t._v(t._s(e.address2)+" ")])]),a("div",[a("div",{staticClass:"i-history-body-price"},[a("q-icon",{attrs:{name:"phone"}}),t._v(" "+t._s(e.storePhone)+" ")],1),a("div",{staticClass:"i-history-body-price"},[a("q-icon",{attrs:{name:"mail"}}),a("div",[t._v(" "+t._s(e.storeEmail)+" ")])],1)])])])])])}),0)],1)],1)])],1)},o=[],r=a("4c23"),s={name:"Stores",components:{},data:function(){return{locations:[],tab:"list"}},mounted:function(){var t=this;r["a"].getLocations().then(function(e){t.locations=e,t.$refs.mapRef.$mapPromise.then(function(t){t.panTo({lat:e[0].latitude,lng:e[0].longitude})})})},computed:{contentStyle:function(){return{}},contentActiveStyle:function(){return{}},thumbStyle:function(){return{right:"2px",borderRadius:"5px",backgroundColor:"#58595B",width:"5px",opacity:.75}}},created:function(){var t=this;r["a"].getLocations().then(function(e){t.locations=e,t.$refs.mapRef.$mapPromise.then(function(t){t.panTo({lat:e[0].latitude,lng:e[0].longitude})})})},methods:{refresh:function(t){var e=this;r["a"].getLocations().then(function(a){e.locations=a,t()})}}},i=s,c=(a("0fcc"),a("2877")),u=Object(c["a"])(i,n,o,!1,null,null,null);e["default"]=u.exports},"0fcc":function(t,e,a){"use strict";var n=a("6005"),o=a.n(n);o.a},"4c23":function(t,e,a){"use strict";a("551c"),a("06db");var n=a("bc3a"),o=a.n(n),r=a("5a75"),s=o.a.create({baseURL:"https://verilifeapi.tenging.is/api"});localStorage.getItem("jwtToken")&&(o.a.defaults.headers.common["Authorization"]="Bearer "+localStorage.getItem("jwtToken"));var i=function(t){var e=function(t){return t.data&&t.data.token&&(localStorage.setItem("jwtToken",t.data.token),o.a.defaults.headers.common["Authorization"]="Bearer "+localStorage.getItem("jwtToken")),console.debug("Request Successful!",t),t.data},a=function(t){return t.response?(console.error("Status:",t.response.status),console.error("Data:",t.response.data),console.error("Headers:",t.response.headers),401===t.response.status?r["a"].$emit("SESSION_TIMEOUT"):r["a"].$emit("SERVER_ERROR",t.response.data.error)):console.error("Error Message:",t.message),Promise.reject(t.response||t.message)};return s(t).then(e).catch(a)},c=i,u="/contact/";function l(t){return c({url:u+"ContactGetByCardIdV2",method:"GET",params:{CardID:t}})}function d(t,e){return c({url:u+"PublishedOffersGet",method:"GET",params:{CardID:t,Company:e}})}function m(t){return c({url:u+"ContactUpdate",method:"POST",data:t})}function f(t){return c({url:u+"ContactRegister",method:"POST",params:{RegInfo:t}})}function p(t,e,a){return c({url:u+"contactChangePassword",method:"POST",params:{AccountID:t,Old:e,New:a}})}function h(t){return c({url:u+"ContactSetAttributes",method:"POST",data:{json:t}})}function g(t){return c({url:u+"ContactGetAttributes",method:"GET",params:{AccountID:t}})}function b(t,e,a){return c({url:u+"contactRegisterByEmail",method:"POST",params:{Name:t,Email:e,Password:a}})}function C(t){return c({url:u+"ContactLogin",method:"POST",params:{LoginInfo:t}})}function y(t){return c({url:u+"GetContactByExternalID",method:"GET",params:{ExternalID:t}})}function G(t){return c({url:u+"ResetPassword",method:"GET",params:{Email:t}})}function v(t){return c({url:u+"NotificationsGetByContactId",method:"GET",params:{ContactID:t}})}function E(t){return c({url:u+"GetRefundList",method:"GET",params:{ContactID:t}})}function T(t){return c({url:u+"GetPublishedItems",method:"GET",params:{ContactID:t}})}function I(t){return c({url:u+"GetItemDetails",method:"GET",params:{ItemNo:t}})}function S(t,e){return c({url:u+"getItemsPerGroup",method:"GET",params:{GroupCode:t,Page:e}})}function P(t,e,a,n){return c({url:u+"GetDigitalReceiptJSON",method:"GET",params:{StoreNo:t,PosTerminalNo:e,TransactionNo:a,Company:n}})}function R(){return c({url:u+"GetFeaturedWatch",method:"GET"})}function w(){return c({url:u+"GetLocations",method:"GET"})}function k(t){return c({url:u+"ContactCheckByEmail",method:"GET",params:{Email:t}})}function _(t){return c({url:u+"GetAddressByZip",method:"GET",params:{Code:t}})}function O(t){return c({url:u+"GetOfferHtml",method:"GET",params:{Code:t}})}var B={getOfferHtml:O,getAddressByZip:_,contactCheckByEmail:k,contactGetByCardId:l,publishedOffersGet:d,contactUpdate:m,getContactByExternalID:y,contactRegister:f,contactLogin:C,resetPassword:G,contactChangePassword:p,notificationsGetByContactId:v,contactRegisterByEmail:b,getPublishedItems:T,contactSetAttributes:h,contactGetAttributes:g,getItemDetails:I,getItemsPerGroup:S,getDigitalReceiptJSON:P,getRefundList:E,getFeaturedWatch:R,getLocations:w};e["a"]=B},"5a75":function(t,e,a){"use strict";var n=a("2b0e"),o=new n["default"];e["a"]=o},6005:function(t,e,a){}}]);