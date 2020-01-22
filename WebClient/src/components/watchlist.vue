<template>

<div class="i-watch-list" ref="watchListRef">
  <watch :item="selectedWatch" :open="watchDialog" @close="watchDialog = false"/>
      <q-scroll-observer @scroll="scrollHandler" />

  <div class="i-watch-list-container" v-for="(watch, idx) in list" :key="idx" >
    <div class="i-watch-list-container-img">
      <q-img @click="openWatch(watch)"
      :src="getItemImageUrl(watch.id)"
      :ratio="1"
      spinner-color="grey-5"
      style="max-width: 150px; height: 150px;"
    />
    </div>
    <div class="i-watch-list-container-descr">
      {{watch.description}}
    </div>
  </div>
</div>

</template>

<style>
.i-watch-list {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: center;
}
.i-watch-list-container {
  width:240px;
  height:220px;
  border: 2px solid #D5D6D7;
  display:flex;
  flex-direction: column;
  margin: 10px;
  padding: 10px;
  cursor: pointer;
}
.i-watch-list-container-img{
  display: flex;
  flex-direction: row;
  justify-content: center;
}
.i-watch-list-container-descr{
  text-align: center;
margin-top:10px;
padding-left:10px;
padding-right:10px;

  color:#58595B;
  font-weight: bold;
}
</style>

<script>
import watch from '../components/watch'

export default {
  name: 'WatchList',
  components: {
    watch: watch
  },
  props: ['list'],
  data () {
    return {
      selectedWatch: null,
      watchDialog: false,
      previousPer: 0
    }
  },
  created: function () {
  },
  methods: {
    scrollHandler (data) {
      var me = this
      if (data.direction !== 'down') {
        return
      }
      var percent = (data.position + window.innerHeight) / me.$refs.watchListRef.offsetHeight
      if (percent > 1) {
        if (me.stop) {
          return
        }
        me.$emit('loadNext')
        me.stop = true
        setTimeout(function () {
          me.stop = false
        }, 400)
      }
    },
    openWatch (item) {
      var me = this
      me.selectedWatch = item
      me.watchDialog = true
    },
    getItemImageUrl (id) {
      return process.env.API + '/images/item/' + id
    }
  }
}
</script>
