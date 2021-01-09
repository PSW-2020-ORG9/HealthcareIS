let server = // #put "'" + SERVER_URL + "'"

let api = server + '/api'

let userService = api + '/user'
let scheduleService = api + '/schedule'
let feedbackService = api + '/feedbacks'
let hospitalService = api + '/hospital'

let examinationUrl = scheduleService + '/examination'
let examinationSearchUrl = scheduleService + '/examination/search'

let roomUrl = hospitalService + '/room/'
let prescriptionsUrl = hospitalService + '/docsearch/prescription'

let feedbacksUrl = feedbackService + '/feedback'

let doctorUrl = userService + '/doctor'
let specialtyUrl = userService + '/specialty'
let patientExaminationsUrl = scheduleService + '/examination'
let patientUrl = userService + '/patient'
let registrationUrl = patientUrl + '/register'
let doctorAvailabilityUrl = userService + '/available/'
let citiesByCountryId = userService + '/city/by-country/'
let countriesUrl= userService +'/country'
let patientAccountUrl = userService + '/patient/account'
let advertisementsUrl = userService + '/advertisement/active'

let patientExaminationSurveyResponseUrl = feedbackService + "/survey/examination"
let patientExaminationsSurveyResponsesUrl = feedbackService + "/survey/examination/multiple"
let surveyUrl = feedbackService + '/survey'
let surveyPreviewUrl = feedbackService + '/survey/preview/admin/1'

let imageUploadUrl = 'https://api.imgur.com/3/image'
let clientId = '9a86c8e89e7d2ea'
let examinationRecommendationUrl = patientExaminationsUrl + '/recommend'

let loginUrl = userService + '/auth/login'


export default{
    feedback: feedbacksUrl ,
    patient: patientUrl,
    specialty:specialtyUrl,
    patientAccount: patientAccountUrl,
    patientRegistration: registrationUrl,
    surveyPreview: surveyPreviewUrl,
    advertisement: advertisementsUrl,

    countries:countriesUrl,
    citiesByCountry:citiesByCountryId,

    survey: surveyUrl,

    examinationSearch: examinationSearchUrl,
    examination: examinationUrl,
    examinationRecommendationUrl : examinationRecommendationUrl,
    prescriptions: prescriptionsUrl,

    docSearchPrescriptionSimple: prescriptionsUrl + '/simple',
    docSearchExaminationSimple: examinationSearchUrl + '/simple',

    docSearchPrescriptionAdvanced: prescriptionsUrl + "/advanced",
    docSearchExaminationAdvanced: examinationSearchUrl + "/advanced",

    imageUpload: imageUploadUrl,
    authorization: 'Client-ID ' + clientId,

    patientExaminations: patientExaminationsUrl,

    patientExaminationSurveyResponseUrl: patientExaminationSurveyResponseUrl,
    patientExaminationsSurveyResponsesUrl: patientExaminationsSurveyResponsesUrl,

    room : roomUrl,

    loginUrl : loginUrl,

    availableDoctorUrl:function(date){
        return doctorAvailabilityUrl + 'doctor?date=' + date
    },
    availableIntervalUrl:function(date,doctorId){
        return doctorAvailabilityUrl + 'interval?date=' + date + '&doctorId=' + doctorId
    },
    doctorBySpecialtyUrl: function (specialtyId) {
        return doctorUrl + "/specialty/" + specialtyId
    }
}