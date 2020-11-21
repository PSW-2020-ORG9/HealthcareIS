import { createRouter, createWebHashHistory } from 'vue-router'
import CreateFeedback from '../views/CreateFeedback.vue'
import ObserveFeedback from '../views/ObserveFeedback.vue'
import SurveyPreview from '../views/SurveyPreview.vue'

const routes = [
  {
    path: '/',
    name: 'ObserveFeedback',
    component: ObserveFeedback
  },
  {
    
    path: '/create-feedback',
    name: 'CreateFeedback',
    component: CreateFeedback
    
  }
  ,
  {
    path:'/survey-preview',
    name:'SurveyPrevie',
    component: SurveyPreview
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
