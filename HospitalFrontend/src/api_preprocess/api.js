let server = // #put "'" + SERVER_URL + "'"

    let api = server + "/api"

let examinationUrl = api + '/examination'
let examinationSearchUrl = api + '/examination/search'

let roomUrl = api + '/room/'
let prescriptionsUrl = api + '/docsearch/prescription'

let feedbacksUrl = api + '/feedback'

let doctorUrl = api + '/doctor'
let specialtyUrl = api + '/specialty'
let patientExaminationsUrl = api + '/examination'
let patientUrl = api + '/patient'
let registrationUrl = patientUrl + '/register'
let doctorAvailabilityUrl = api + '/available/'
let citiesByCountryId = api + '/city/by-country/'
let countriesUrl= api +'/country'
let patientAccountUrl = api + '/patient/account'

let patientExaminationSurveyResponseUrl = api + "/survey/examination"
let patientExaminationsSurveyResponsesUrl = api + "/survey/examination/multiple"
let surveyUrl = api + '/survey'
let surveyPreviewUrl = api+'/survey/preview/admin/1'

let imageUploadUrl = 'https://api.imgur.com/3/image'
let clientId = '9a86c8e89e7d2ea'
let examinationRecommendationUrl = patientExaminationsUrl + '/recommend'


export default{
    feedback: feedbacksUrl ,
    patient: patientUrl,
    specialty:specialtyUrl,
    patientAccount: patientAccountUrl,
    patientRegistration: registrationUrl,
    surveyPreview: surveyPreviewUrl,

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