<template>
 <q-page class="flex row i-splash-page">
   <img class="i-splash-logo" src="/statics/logo-splash.png"/>
   <div class="i-splash-container">
    <div class="i-splash-body">
      <div class="i-splash-header">
        <div class="i-splash-header-l1">
          THE OFFICIAL INVICTA LOYALTY PROGRAM
        </div>
        <div class="i-splash-header-l2">
          <span>Silver Or Platinum, Which Will You Choose?</span>
        </div>
      </div>
      <div class="i-splash-main">

        <div class="i-splash-body-right">
          <div class="i-splash-body-right-text">
            <div class="i-splash-body-right-text-line">
              <div>
                <img class="i-left-img" src="/statics/Silver_white_shadow.png"/>
                </div>
              <div>
               Silver status earns you rewards and early access to special Invicta deals.
               </div>
            </div>
            <div class="i-splash-body-right-text-line">
              <div>
                <img class="i-left-img" src="/statics/Platinum_white_shadow.png" style="width:15%"/>
                </div>
              <div>
               Upgrade to Platinum status and get exclusive promotions, specialized gifts, and VIP service.
               </div>
            </div>
             <div class="i-splash-body-right-text-line" >
              <div>
                <img class="i-left-img" src="/statics/download-splash.png"/>
                </div>
              <div>
               Click here to download the Invicta Loyalty App NOW!
               </div>
               <div >
                 <a href="https://apps.apple.com/app/id1479286760"><img class="i-left-img" src="/statics/App_Store.png"/> </a>
                 <a href="https://play.google.com/store/apps/details?id=com.tenging.invicta"><img class="i-left-img-2" src="/statics/Google_Play.png"/> </a>
               </div>
            </div>
            <div class="i-splash-body-right-text-line q-pt-sm">
              <span @click="gotologin" class="i-bro-continue">Continue with Browser</span>
            </div>
          </div>
        </div>
      </div>
   </div>
   </div>
</q-page>
</template>

<script>

export default {
  name: 'Splash',
  data () {
    return {
    }
  },
  created () {
  },
  methods: {
    gotologin () {
      this.$router.push('/login')
    },
    gotoAppStore () {
      window.location = 'https://apps.apple.com/app/id1479286760'
    }
  }
}
</script>

<style>
.i-splash-logo{
    position: absolute;
    right: 2%;
    bottom: 3%;
    width: 6%;
}
.i-splash-body-right-text-line{
  margin-top:4%;
}

.i-left-img-2{
  width:10%;
  margin-left:4%;
  margin-top:2%;
}
.i-left-img{
  width:10%;
}
.i-splash-body-right-text{
  width:35%;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  color:#fffffe;
  text-transform: uppercase;
  font-size:0.9vw;
  padding-left: 9%;
}
.i-splash-header-l1{
  text-align: center;
  color:#fffffe;
  font-size: 1.4vw;
}
.i-splash-header-l2{
  text-align: center;
  color:#fff200;
}
.i-bro-continue {
 color:#fff200;
 border: 2px solid #fff200;
 padding:5px;
 margin-top:5%;
 cursor:pointer;
}

.i-splash-header-l2 span {
  border-top: 3px solid #fff200;
  border-bottom: 3px solid #fff200;
  font-size:2.4vw;
  text-transform: uppercase;
}
.i-splash-body-right{
  display: flex;
  width:100%;
    background-image:    url("/statics/watches.png");
  background-size: 80%;
  background-repeat:   no-repeat;
 background-position: right center;
 flex-direction: column;
    justify-content: center;
}

.i-splash-main{
  display: flex;
  flex-direction: row;
  height: 70%;

}
.i-splash-header{
  display: flex;
  width:100%;
  height: 20%;
  flex-direction: column;
  justify-content: flex-end;
}
.i-splash-container{
  display: flex;
  flex-direction: row;
  justify-content: center;
  width: 100%;
}
.i-splash-body{
  display: flex;
  width:70%;
  flex-direction: column;
  margin-right: 9%;
}
.i-splash-page {
  font-family: Arimo;
  background-color: #333333;
  background-image:    url("/statics/splash-bg.png");
  background-size: cover;
  background-repeat:   no-repeat;
 background-position: center center;
 font-family: DinMedium;
}
/* 800px or less */
@media only screen and (max-width: 800px) {
  .i-splash-body-right {
    background-image: none;
  }
  .i-splash-body-right-text {
    width: 100%;
  }
  .i-left-img {
    width: 5%;
  }
  .i-left-img-2 {
     width: 5%;
  }
  .i-splash-body-right-text{
    font-size: 1.9vw;
  }
}
/* 415px or less */
@media only screen and (max-width: 415px) {
  .i-splash-body-right {
    background-image: none;
  }
  .i-splash-body-right-text {
    width: 100%;
  }
  .i-left-img {
    width: 10%;
  }
  .i-left-img-2 {
     width: 10%;
  }
  .i-splash-body-right-text{
    font-size: 3.9vw;
  }
  .i-splash-header-l1 {
    font-size: 3vw;
  }
  .i-splash-header-l2 span{
font-size: 3.6vw;
  }
  .i-splash-logo{
        width: 16%;
  }
  .i-splash-header-l2{
    margin-top:3%;
  }
}
</style>
