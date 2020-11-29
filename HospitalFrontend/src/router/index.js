import { createRouter, createWebHashHistory } from 'vue-router'
import CreateFeedback from '../views/CreateFeedback.vue'
import ObserveFeedback from '../views/ObserveFeedback.vue'
import SurveyPreview from '../views/SurveyPreview.vue'
import ObserveMedicalRecord from '../views/ObserveMedicalRecord.vue'
import DocSearch from '../views/DocSearch.vue'
import CreateSurveyResponse from '../views/CreateSurveyResponse.vue'

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
  },
  {
    path: '/medical-record',
    name: 'ObserveMedicalRecord',
    component: ObserveMedicalRecord
  },
  {
    path: '/doc-search',
    name: 'DocSearch',
    component: DocSearch
  }
  ,
  {
    path:'/survey-preview',
    name:'SurveyPreview',
    component: SurveyPreview
  }
  ,
  {
    path: '/survey',
    name: 'CreateSurveyResponse',
    component: CreateSurveyResponse
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
