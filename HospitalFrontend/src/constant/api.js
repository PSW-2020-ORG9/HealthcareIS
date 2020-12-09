let server='http://localhost:5000'
let examinationsUrl = server + '/docsearch/examination'
let prescriptionsUrl = server + '/docsearch/prescription'
let feedbacksUrl = server + '/feedback'
let patientUrl =   server + '/patient'
let patientAccountUrl = server + '/patient/account'
let surveyPreviewUrl = server+'/survey/preview/admin/1'
let imageUploadUrl = 'https://api.imgur.com/3/image'
let clientId = '9a86c8e89e7d2ea'
let countriesUrl=server+'/country'
let citiesByCountryId = server + '/city/by-country/'
let registrationUrl = patientUrl + '/register'
let surveyUrl = server + '/survey'
let patientExaminationsUrl = server + '/examination'

let patientExaminationSurveyResponseUrl = server + "/survey/examination/"
let patientExaminationsSurveyResponsesUrl = server + "/survey/examination/multiple"

export default{
    feedback: feedbacksUrl ,
    patient: patientUrl,
    patientAccount: patientAccountUrl,
    patientRegistration: registrationUrl,
    surveyPreview: surveyPreviewUrl,

    countries:countriesUrl,
    citiesByCountry:citiesByCountryId,
    
    survey: surveyUrl,
    
    examinations: examinationsUrl,
    prescriptions: prescriptionsUrl,

    docSearchPrescriptionSimple: prescriptionsUrl + '/simple',
    docSearchExaminationSimple: examinationsUrl + '/simple',

    docSearchPrescriptionAdvanced: prescriptionsUrl + "/advanced",
    docSearchExaminationAdvanced: examinationsUrl + "/advanced",

    imageUpload: imageUploadUrl,
    authorization: 'Client-ID ' + clientId,

    patientExaminations: patientExaminationsUrl,

    patientExaminationSurveyResponseUrl: patientExaminationSurveyResponseUrl,
    patientExaminationsSurveyResponsesUrl: patientExaminationsSurveyResponsesUrl
}