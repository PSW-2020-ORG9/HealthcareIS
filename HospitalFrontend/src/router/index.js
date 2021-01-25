import { createRouter, createWebHashHistory } from 'vue-router'
import CreateFeedback from '../views/CreateFeedback.vue'
import ObserveFeedback from '../views/ObserveFeedback.vue'
import ObserveMedicalRecord from '../views/ObserveMedicalRecord.vue'
import DocSearch from '../views/DocSearch.vue'
import SurveyPreview from '../views/SurveyPreview.vue'
import SchedulingStatistics from '../views/SchedulingStatistics.vue'
import RegisterPatient from '../views/registration/RegisterPatient.vue'
import PersonalInfromation from '../views/registration/PersonalInformation.vue'
import HealthStatus from '../views/registration/HealthStatus.vue'
import AccountDetails from '../views/registration/AccountDetails.vue'
import ProfilePicture from '../views/registration/ProfilePicture.vue'
import SuccessfullyRegistered from '../views/registration/SuccessfullyRegistered.vue'
import CreateSurveyResponse from '../views/CreateSurveyResponse.vue'
import ScheduleAppointment from '../views/scheduling/default-scheduling/ScheduleAppointment.vue'
import ChooseDate from '../views/scheduling/default-scheduling/ChooseDate.vue'
import ChooseDoctor from '../views/scheduling/default-scheduling/ChooseDoctor.vue'
import ChooseAppointment from '../views/scheduling/default-scheduling/ChooseAppointment.vue'
import SchedulingType from '../views/scheduling/SchedulingType.vue'
import RecommendAppointment from '../views/scheduling/recommendation/RecommendAppointment.vue'
import AppointmentPreferences from '../views/scheduling/recommendation/AppointmentPreferences.vue'
import ChooseRecommended from '../views/scheduling/recommendation/ChooseRecommended.vue'
import ObservePatientExaminations from '../views/ObservePatientExaminations.vue'
import HomePage from '../views/HomePage.vue'
import LoginForm from '../views/LoginForm.vue'
import { parseJwt } from '../jwt.js'

const routes = [
  {
    path: '/feedbacks',
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
  }
  ,
  {
    path:'/scheduling-statistics',
    name:'SchedulingStatistics',
    component: SchedulingStatistics

  }
  ,
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
    path:'/scheduling-type',
    component:SchedulingType,
    name:'schedulingType'
  }
  ,
  {
    path:'/schedule-appointment',
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
    path:'/recommend-appointment',
    component:RecommendAppointment,
    children:[
      {
        path:'',
        name:'appointmentPreferences',
        component:AppointmentPreferences
      }
      ,
      {
        path:'choose-recommended',
        name:'chooseRecommended',
        component:ChooseRecommended
      }
    ]
  }
  ,
  {
    path: '/survey/:doctor/:examination',
    name: 'CreateSurveyResponse',
    component: CreateSurveyResponse
  }
  ,
  {
    path: '/examinations',
    name: 'ObservePatientExaminations',
    component: ObservePatientExaminations
  }
  ,
  {
    path: '/',
    name: 'HomePage',
    component: HomePage
  },
  {
    path: '/login',
    name: 'LoginForm',
    component: LoginForm
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  if (to.name === 'LoginForm' 
    || to.name === 'personalInformation'
    || to.name === 'healthStatus'
    || to.name === 'accountDetails'
    || to.name === 'profilePicture') {
    next()
  } else {
    let authCookie = document.cookie
    let user = null
    let cookieValid = true
    if (!authCookie) {
      cookieValid = false
    } else {
      user = parseJwt(document.cookie.split('=')[1]);
      let epochMicrotimeDiff = Math.abs(new Date(0, 0, 1).setFullYear(1));

      if (new Date() > new Date(user.exp / 10000 - epochMicrotimeDiff)) {
        cookieValid = false
      }
    }
    
    if (!cookieValid) next({name: 'LoginForm'})
    else if (!(to.name == 'HomePage' || to.name == 'ObserveFeedback' || to.name == 'SurveyPreview' || to.name == 'SchedulingStatistics') && user.role == 'Admin') {
      next({ name: 'ObserveFeedback' })
    } else {
      next()
    }
  }
})

export default router
