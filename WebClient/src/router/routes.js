
const routes = [
  {
    path: '/dashboard',
    component: () => import('layouts/MyLayout.vue'),
    children: [
      { path: '', component: () => import('pages/Contact.vue') },
      { path: 'contact', component: () => import('pages/Contact.vue') },
      { path: 'contact/:tab', component: () => import('pages/Contact.vue') },
      { path: 'history', component: () => import('pages/History.vue') },
      { path: 'notifications', component: () => import('pages/Notifications.vue') },
      { path: 'home', component: () => import('pages/home.vue') },
      { path: 'collections', component: () => import('pages/Collections.vue') },
      { path: 'stores', component: () => import('pages/Stores.vue') }
    ]
  },
  {
    path: '/',
    component: () => import('layouts/LoginLayout.vue'),
    children: [
      { path: '', component: () => import('pages/Login.vue') },
      { path: 'splash', component: () => import('pages/Splash.vue') },
      { path: 'login', component: () => import('pages/Login.vue') },
      { path: '/:id', component: () => import('pages/Register.vue') },
      { path: 'register', component: () => import('pages/Register.vue') }
    ]
  }
]

// Always leave this as last one
if (process.env.MODE !== 'ssr') {
  routes.push({
    path: '*',
    component: () => import('pages/Error404.vue')
  })
}

export default routes
