import {createStore} from 'vuex'

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
  },
  mutations: {
    setPersonalInformation(state,payload){
      state.patientRegistrationDto.personalInformation = payload
    }
    ,
    setAccountDetails(state,payload){
      state.patientRegistrationDto.accountDetails = payload
    }
  },
  actions: {
  },
  modules: {
  }
})
