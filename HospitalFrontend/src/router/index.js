import { createRouter, createWebHashHistory } from 'vue-router'
import CreateFeedback from '../views/CreateFeedback.vue'
import ObserveFeedback from '../views/ObserveFeedback.vue'
import ObserveMedicalRecord from '../views/ObserveMedicalRecord.vue'
import DocSearch from '../views/DocSearch.vue'
import SurveyPreview from '../views/SurveyPreview.vue'
import RegisterPatient from '../views/registration/RegisterPatient.vue'
import PersonalInfromation from '../views/registration/PersonalInformation.vue'
import HealthStatus from '../views/registration/HealthStatus.vue'
import AccountDetails from '../views/registration/AccountDetails.vue'
import ProfilePicture from '../views/registration/ProfilePicture.vue'
import SuccessfullyRegistered from '../views/registration/SuccessfullyRegistered.vue'
import CreateSurveyResponse from '../views/CreateSurveyResponse.vue'
import ScheduleAppointment from '../views/scheduling/ScheduleAppointment.vue'
import ChooseDate from '../views/scheduling/ChooseDate.vue'
import ChooseDoctor from '../views/scheduling/ChooseDoctor.vue'
import ChooseAppointment from '../views/scheduling/ChooseAppointment.vue'

const routes = [
  {
    path: '/',
    name: 'ObserveFeedback',
    component: ObserveFeedback
  },
  {
    path: '/successfully-registered',
    name: 'SuccessfullyRegistered',
    component: SuccessfullyRegistered
  }
  ,
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
  },
  {
    path:'/survey-preview',
    name:'SurveyPreview',
    component: SurveyPreview
  },
  {
    path:'/register',
    component: RegisterPatient,
    children:[
      {
        path:'',
        name:'personalInformation',
        component:PersonalInfromation
      }
      ,
      {
        path:'health-status',
        name:'healthStatus',
        component:HealthStatus
      }
      ,
      {
        path:'account-details',
        name:'accountDetails',
        component:AccountDetails
      }
      ,
      {
        path:'profile-picture',
        name:'profilePicture',
        component: ProfilePicture
      }
    ]
  }
  ,
  {
    path:'/schedule',
    component: ScheduleAppointment,
    children:[
      {
        path:'',
        name:'chooseDate',
        component:ChooseDate
      }
      ,
      {
        path:'choose-doctor',
        name:'chooseDoctor',
        component:ChooseDoctor
      }
      ,
      {
        path:'choose-appointment',
        name:'chooseAppointment',
        component:ChooseAppointment
      }
    ]
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
