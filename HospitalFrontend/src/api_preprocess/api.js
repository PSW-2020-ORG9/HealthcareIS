let server = // #put "'" + SERVER_URL + "'"
let examinationsUrl = server + '/api/docsearch/examination'
let prescriptionsUrl = server + '/api/docsearch/prescription'
let feedbacksUrl = server + '/api/feedback'
let patientUrl =   server + '/api/patient'
let patientAccountUrl = server + '/api/patient/account'
let surveyPreviewUrl = server+'/api/survey/preview/admin/1'
let imageUploadUrl = 'https://api.imgur.com/3/image'
let clientId = '9a86c8e89e7d2ea'
let countriesUrl=server+'/api/country'
let citiesByCountryId = server + '/api/city/by-country/'
let registrationUrl = patientUrl + '/api/register'
let surveyUrl = server + '/api/survey'
let patientExaminationsUrl = server + '/api/examination'
let examinationRecommendationUrl = patientExaminationsUrl + '/api/recommend'

let patientExaminationSurveyResponseUrl = server + "/api/survey/examination/"
let patientExaminationsSurveyResponsesUrl = server + "/api/survey/examination/multiple"
let doctorAvailabiltyUrl = server + '/api/available/'
let examinationUrl = server + '/api/examination'
let doctorUrl = server + '/api/doctor'
let specialtyUrl = server + '/api/specialty'

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

    examinations: examinationsUrl,
    examination: examinationUrl,
    examinationRecommendationUrl : examinationRecommendationUrl,
    prescriptions: prescriptionsUrl,

    docSearchPrescriptionSimple: prescriptionsUrl + '/simple',
    docSearchExaminationSimple: examinationsUrl + '/simple',

    docSearchPrescriptionAdvanced: prescriptionsUrl + "/advanced",
    docSearchExaminationAdvanced: examinationsUrl + "/advanced",

    imageUpload: imageUploadUrl,
    authorization: 'Client-ID ' + clientId,

    patientExaminations: patientExaminationsUrl,

    patientExaminationSurveyResponseUrl: patientExaminationSurveyResponseUrl,
    patientExaminationsSurveyResponsesUrl: patientExaminationsSurveyResponsesUrl,

    availableDoctorUrl:function(date){
        return doctorAvailabiltyUrl + 'doctor?date=' + date
    },
    availableIntervalUrl:function(date,doctorId){
        return doctorAvailabiltyUrl + 'interval?date=' + date + '&doctorId=' + doctorId
    },
    doctorBySpecialtyUrl: function (specialtyId) {
        return doctorUrl + "/specialty/" + specialtyId
    }
}