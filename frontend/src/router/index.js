import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import ProfileView from '@/views/ProfileView.vue'
import AdminView from '@/views/AdminView.vue'
import StatisticsView from '@/views/StatisticsView.vue'
import { useAuthStore } from '@/stores/auth'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/lk',
      name: 'profile',
      component: ProfileView,
      meta: {
        authRequired: true
      }
    },
    {
      path: '/admin',
      name: 'admin',
      component: AdminView,
      meta: {
        authRequired: true,
        adminRequired: true
      }
    },
    {
      path: '/statistics',
      name: 'statistics',
      component: StatisticsView,
      meta: {
        authRequired: true,
        adminRequired: true
      }
    }
  ]
})

router.beforeEach(async (to, from, next) => {
  const authStore = useAuthStore()

  if (!authStore.isInit) {
    await authStore.updateCurrentUser()
  }

  if (to.meta.authRequired) {
    if (!authStore.isAuth) {
      router.push("/")
      return
    }
  }
  if (to.meta.adminRequired) {
    if (authStore.currentUser.role != 2) {
      router.push("/")
      return
    }
  }
  if (to.matched.length == 0) {
    router.push("/")
    return
  }
  return next()
})

export default router
