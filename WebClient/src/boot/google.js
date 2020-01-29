import * as VueGoogleMaps from 'vue2-google-maps'

export default async ({ Vue }) => {
  Vue.use(VueGoogleMaps, {
    load: {
      key: 'AIzaSyCAUMahgNyDzeJBJIm4OYRI6qzhyT9tAjk',
      libraries: 'places'
    },
    installComponents: true
  })
}
