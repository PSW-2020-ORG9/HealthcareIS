import {createStore} from 'vuex'
import {v4 as uuid} from 'uuid'

export default createStore({
  state: {
    patientRegistrationDto:{
      personalInformation:{
        Name:'',
        MiddleName:'',
        Surname:'',
        Jmbg:'',
        DateOfBirth:'',
        TelephoneNumber:'',
        Address:'',
        MaritalStatus:'',
        Gender:'',
        CountryOfResidenceId:0,
        CityOfResidenceId:0,
        CountryOfBirthId:0,
        CityOfBirthId:0,
      },
      accountDetails:{
        Username:'',
        Password:'',
        Email:'',
      }      
    }
    ,
    scheduledExaminationDTO:{
      StartTime:'',
      DoctorId:'',
      PatientId:1
    },
    user: {
      name: '',
      surname: '',
      age:0
    },
    availableDoctors:[]
    ,
    selectedDate:null
    ,
    availableAppointments:[]
    ,
    recommendationDto:null,
    schedulingSessionUuid:null 
  },
  mutations: {
    setPersonalInformation(state,payload){
      state.patientRegistrationDto.personalInformation = payload
    }
    ,
    setAccountDetails(state,payload){
      state.patientRegistrationDto.accountDetails = payload
    }
    ,
    setAvailableDoctors(state,payload){
      state.availableDoctors = payload
    }
    ,
    setSelectedDate(state,payload){
      state.selectedDate = payload
    }
    ,
    setDoctorId(state,payload){
      state.scheduledExaminationDTO.DoctorId=payload
    }
    ,
    setAvailableAppointments(state,payload){
      state.availableAppointments=payload
    }
    ,
    clearAppointmentInfo(state){
      state.availableDoctors=[]
      state.selectedDate=null
      state.availableAppointments=[]
    }
    ,
    clearPatientRegistrationDto(state){
      this.patientRegistrationDto = {
        personalInformation:{
          Name:'',
          MiddleName:'',
          Surname:'',
          Jmbg:'',
          DateOfBirth:'',
          TelephoneNumber:'',
          Address:'',
          MaritalStatus:'',
          Gender:'',
          CountryOfResidenceId:0,
          CityOfResidenceId:0,
          CountryOfBirthId:0,
          CityOfBirthId:0,
        },
        accountDetails:{
          Username:'',
          Password:'',
          Email:'',
        }    
      }
    }
    ,
    setRecommendationDto(state,payload){
      state.recommendationDto = payload
    },
    setUser(state, payload) {
      state.user = payload
    },
    generateUuid(state){
      state.schedulingSessionUuid = uuid()
    }
  },
  actions: {
  },
  modules: {
  }
})
