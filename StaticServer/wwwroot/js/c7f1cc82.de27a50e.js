(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["c7f1cc82"],{"0d02":function(t,e,i){},"21ab":function(t,e,i){},"2b3b":function(t,e,i){"use strict";var n=function(){var t=this,e=t.$createElement,i=t._self._c||e;return i("q-dialog",{attrs:{square:!0,persistent:"",maximized:t.maximizedToggle,"transition-show":"slide-up","transition-hide":"slide-down"},on:{hide:function(e){return t.$emit("close")}},model:{value:t.dialogOpen,callback:function(e){t.dialogOpen=e},expression:"dialogOpen"}},[i("q-resize-observer",{on:{resize:t.onResize}}),i("div",{staticClass:"i-watch-container"},[i("div",{staticClass:"i-watch-close"},[i("q-btn",{directives:[{name:"close-popup",rawName:"v-close-popup"}],attrs:{icon:"close",flat:"",round:"",dense:""},on:{click:function(e){return t.$emit("close")}}})],1),i("div",{staticClass:"i-watch-item"},[i("div",{staticClass:"i-watch-img"},[t.item?i("img",{staticClass:"t-watch-pic",attrs:{src:t.getItemImageUrl(t.item.id)}}):t._e()]),0!==Object.keys(t.itemDetail).length?i("div",{staticClass:"i-watch-body"},[i("div",{staticClass:"i-watch-body-line q-mb-sm i-watch-body-line-title"},[t._v("\n               "+t._s(t.itemDetail.description)+"\n            ")]),i("div",{staticClass:"i-watch-body-line"},[i("div",{staticClass:"i-watch-body-line-left"},[t._v("\n                No:\n              ")]),i("div",{staticClass:"i-watch-body-line-right"},[t._v("\n               "+t._s(t.itemDetail.no)+"\n              ")])]),i("div",{staticClass:"i-watch-body-line"},[i("div",{staticClass:"i-watch-body-line-left"},[t._v("\n                Description\n              ")]),i("div",{staticClass:"i-watch-body-line-right"},[t._v("\n               "+t._s(t.itemDetail.description)+"\n              ")])]),i("div",{staticClass:"i-watch-body-line"},[i("div",{staticClass:"i-watch-body-line-left"},[t._v("\n                Strain Code:\n              ")]),i("div",{staticClass:"i-watch-body-line-right"},[t._v("\n               "+t._s(t.itemDetail.strainCode)+"\n              ")])]),i("div",{staticClass:"i-watch-body-line"},[i("div",{staticClass:"i-watch-body-line-left"},[t._v("\n                Category\n              ")]),i("div",{staticClass:"i-watch-body-line-right"},[t._v("\n               "+t._s(t.itemDetail.itemCatefory)+"\n              ")])]),i("div",{staticClass:"i-watch-body-line"},[i("div",{staticClass:"i-watch-body-line-left"},[t._v("\n                Product Group:\n              ")]),i("div",{staticClass:"i-watch-body-line-right"},[t._v("\n               "+t._s(t.itemDetail.productGroup)+"\n              ")])]),i("div",{staticClass:"i-watch-body-line"},[i("div",{staticClass:"i-watch-body-line-left"},[t._v("\n                Unit Of Measure:\n              ")]),i("div",{staticClass:"i-watch-body-line-right"},[t._v("\n               "+t._s(t.itemDetail.baseUnitOfMeasure)+"\n              ")])]),i("div",{staticClass:"i-watch-body-line"},[i("div",{staticClass:"i-watch-body-line-left"},[t._v("\n                THC:\n              ")]),i("div",{staticClass:"i-watch-body-line-right"},[t._v("\n               "+t._s(t.itemDetail.THC)+"\n              ")])]),i("div",{staticClass:"i-watch-body-line"},[i("div",{staticClass:"i-watch-body-line-left"},[t._v("\n                CBD:\n              ")]),i("div",{staticClass:"i-watch-body-line-right"},[t._v("\n               "+t._s(t.itemDetail.CBD)+"\n              ")])]),i("div",{staticClass:"i-watch-body-line"},[i("div",{staticClass:"i-watch-body-line-left"},[t._v("\n                CBN:\n              ")]),i("div",{staticClass:"i-watch-body-line-right"},[t._v("\n               "+t._s(t.itemDetail.CBN)+"\n              ")])])]):t._e()])])],1)},a=[],s=(i("6b54"),i("06db"),i("4c23")),o=i("43c2"),c={name:"Watch",components:{},props:["item","open"],data:function(){return{isMobile:!1,contact:{id:""},itemDetail:{},dialogOpen:!1,maximizedToggle:this.$q.platform.is.mobile}},watch:{open:function(t){function e(e){return t.apply(this,arguments)}return e.toString=function(){return t.toString()},e}(function(t){console.log("open: "+open);var e=this;!0===t?(e.$q.loading.show(),s["a"].getItemDetails(e.item.id).then(function(i){console.log("item detail: "),console.log(i),e.itemDetail=i,e.dialogOpen=t},function(t){console.error(t.message),e.dialogOpen=!1}).then(function(){e.$q.loading.hide()})):e.dialogOpen=!1,console.log(e.item)})},created:function(){var t=this,e=o["a"].getUserData();e&&(t.contact=e)},methods:{onResize:function(t){var e=this;t.width<400?e.isMobile=!0:e.isMobile=!1},getItemImageUrl:function(t){return"https://verilifeapi.tenging.is/api/images/item/"+t}}},l=c,r=(i("71e1"),i("2877")),d=Object(r["a"])(l,n,a,!1,null,null,null);e["a"]=d.exports},"4ae8":function(t,e,i){},"71e1":function(t,e,i){"use strict";var n=i("21ab"),a=i.n(n);a.a},"9df0":function(t,e,i){"use strict";var n=i("4ae8"),a=i.n(n);a.a},aa01:function(t,e,i){"use strict";var n=i("0d02"),a=i.n(n);a.a},cdf5:function(t,e,i){"use strict";i.r(e);var n=function(){var t=this,e=t.$createElement,i=t._self._c||e;return i("q-page",{staticClass:"q-pa-sm"},[i("div",{staticClass:"i-coll-container"},[i("div",{directives:[{name:"show",rawName:"v-show",value:"collections"===t.tab,expression:"tab === 'collections'"}],attrs:{name:"collections"}},[i("div",{staticClass:"i-coll-title"},[t._v(" COLLECTIONS ")]),i("q-scroll-area",{staticStyle:{height:"calc(100vh - 150px)"},attrs:{"thumb-style":t.thumbStyle,"content-style":t.contentStyle,"content-active-style":t.contentActiveStyle}},[i("div",{staticClass:"i-group-list"},t._l(t.groups,function(e,n){return i("div",{key:n,class:{"i-group-list-container":!0,"i-group-favorite":!0===e.favorite},on:{click:function(i){return t.groupClicked(e)}}},[i("div",{staticClass:"i-group-heart",on:{click:function(i){return i.stopPropagation(),t.groupFavoriteClicked(e.code)}}},[!0===e.favorite?i("q-icon",{attrs:{name:"favorite"}}):t._e(),!1===e.favorite?i("q-icon",{attrs:{name:"favorite_border"}}):t._e()],1),i("div",{staticClass:"i-group-list-container-img"},[0===e.images.length?i("span",[t._v(" "+t._s(e.webDescription)+" ")]):t._e(),1===e.images.length?i("q-img",{staticStyle:{width:"80%"},attrs:{src:t.getImageUrl(e.images[0].id),"spinner-color":"grey-5"}}):t._e()],1)])}),0)])],1),"watches"===t.tab?i("div",{attrs:{name:"watches"}},[i("div",{staticClass:"i-coll-title"},[t._v(" "+t._s(t.selectedGroupName)+" ")]),i("q-scroll-area",{ref:"scrollArea",staticStyle:{height:"calc(100vh - 150px)"},attrs:{"thumb-style":t.thumbStyle,"content-style":t.contentStyle,"content-active-style":t.contentActiveStyle}},[i("watchlist",{ref:"wl",attrs:{list:t.watchList},on:{loadNext:t.loadMoreItems}})],1)],1):t._e()]),i("q-page-sticky",{directives:[{name:"show",rawName:"v-show",value:"watches"===t.tab,expression:"tab === 'watches'"}],attrs:{position:"top-left",offset:[20,18]}},[i("q-btn",{attrs:{round:"",color:"grey-6",icon:"arrow_back"},on:{click:function(e){t.tab="collections"}}})],1)],1)},a=[],s=(i("7514"),i("2ef0")),o=i.n(s),c=i("4c23"),l=function(){var t=this,e=t.$createElement,i=t._self._c||e;return i("div",{ref:"watchListRef",staticClass:"i-watch-list"},[i("watch",{attrs:{item:t.selectedWatch,open:t.watchDialog},on:{close:function(e){t.watchDialog=!1}}}),i("q-scroll-observer",{on:{scroll:t.scrollHandler}}),t._l(t.list,function(e,n){return i("div",{key:n,staticClass:"i-watch-list-container"},[i("div",{staticClass:"i-watch-list-container-img"},[i("q-img",{staticStyle:{"max-width":"150px",height:"150px"},attrs:{src:t.getItemImageUrl(e.id),ratio:1,"spinner-color":"grey-5"},on:{click:function(i){return t.openWatch(e)}}})],1),i("div",{staticClass:"i-watch-list-container-descr"},[t._v("\r\n      "+t._s(e.description)+"\r\n    ")])])})],2)},r=[],d=i("2b3b"),u={name:"WatchList",components:{watch:d["a"]},props:["list"],data:function(){return{selectedWatch:null,watchDialog:!1,previousPer:0}},created:function(){},methods:{scrollHandler:function(t){var e=this;if("down"===t.direction){var i=(t.position+window.innerHeight)/e.$refs.watchListRef.offsetHeight;if(i>1){if(e.stop)return;e.$emit("loadNext"),e.stop=!0,setTimeout(function(){e.stop=!1},400)}}},openWatch:function(t){var e=this;e.selectedWatch=t,e.watchDialog=!0},getItemImageUrl:function(t){return"https://verilifeapi.tenging.is/api/images/item/"+t}}},h=u,v=(i("aa01"),i("2877")),g=Object(v["a"])(h,l,r,!1,null,null,null),f=g.exports,m=i("43c2"),p={name:"Collections",components:{watchlist:f},data:function(){return{tab:"collections",splitterModel:20,contact:null,json:"",groups:[],offerDialog:!1,selectedOffer:{description:"",items:[]},watchList:[],selectedGroupName:"",currentItemPage:1,selectedGroupCode:0}},computed:{contentStyle:function(){return{}},contentActiveStyle:function(){return{}},thumbStyle:function(){return{right:"2px",borderRadius:"5px",backgroundColor:"#58595B",width:"5px",opacity:.75}}},created:function(){var t=this,e=m["a"].getUserData();e&&(t.contact=e,t.$q.loading.show(),c["a"].getPublishedItems().then(function(e){t.json=e,t.groups=o.a.reverse(o.a.sortBy(e.groups,function(t){return t.images.length})),c["a"].contactGetAttributes(t.contact.id).then(function(e){var i=o.a.find(e,{name:"Favorite Watch Collection"});if(i){var n=o.a.find(t.groups,{code:i.value});n.favorite=!0}})},function(t){console.error(t.message)}).then(function(){t.$q.loading.hide()}))},methods:{groupFavoriteClicked:function(t){var e=this;o.a.each(e.groups,function(t){t.favorite=!1});var i=o.a.find(e.groups,{code:t});i.favorite=!0,e.setFavoriteGroup(i),console.log(i)},loadMoreItems:function(t){var e=this;-1!==e.currentItemPage&&(e.currentItemPage=e.currentItemPage+1,e.$q.loading.show(),c["a"].getItemsPerGroup(e.selectedGroupCode,e.currentItemPage).then(function(i){0===i.items.length&&(e.currentItemPage=-1),o.a.each(i.items,function(t){e.watchList.push(t)}),t&&t()},function(t){console.error(t.message)}).then(function(){e.$q.loading.hide()}))},groupClicked:function(t){var e=this;e.selectedGroupName=t.webDescription,e.selectedGroupCode=t.code,e.currentItemPage=0,e.watchList=[],e.loadMoreItems(function(){e.tab="watches"})},setFavoriteGroup:function(t){var e=this,i={account:e.contact.id,clubCode:"100",attributes:[{name:"Favorite Watch Collection",value:t.code}]};c["a"].contactSetAttributes(JSON.stringify(i)).then(function(){e.$q.notify({color:"grey-6",position:"bottom",message:"Favorite collection selected",icon:"done"})},function(t){console.error(t.message)})},getImageUrl:function(t){return"https://verilifeapi.tenging.is/api/images/"+t}}},w=p,b=(i("9df0"),Object(v["a"])(w,n,a,!1,null,null,null));e["default"]=b.exports}}]);