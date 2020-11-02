import { createRouter, createWebHashHistory } from 'vue-router'
import CreateFeedback from '../views/CreateFeedback.vue'
import ObserveFeedback from '../views/ObserveFeedback.vue'
import PublishFeedback from '../views/PublishFeedback.vue'

const routes = [
  {
    path: '/',
    name: 'ObserveFeedback',
    component: ObserveFeedback
  },
  {
    path: '/publish-feedback',
    name: 'PublishFeedback',
    component: PublishFeedback
  },
  {
    
    path: '/create-feedback',
    name: 'CreateFeedback',
    component: CreateFeedback
    
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
