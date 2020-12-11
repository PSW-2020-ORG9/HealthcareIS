let server = // #put "'http://" + SERVER_URI + "'"
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
let doctorAvailabiltyUrl = server + '/available/'
let examinationUrl = server + '/examination'
let doctorUrl = server + '/doctor'
let departmentUrl = server + '/department'

export default{
    feedback: feedbacksUrl ,
    patient: patientUrl,
    department:departmentUrl,
    patientAccount: patientAccountUrl,
    patientRegistration: registrationUrl,
    surveyPreview: surveyPreviewUrl,

    countries:countriesUrl,
    citiesByCountry:citiesByCountryId,

    survey: surveyUrl,

    examinations: examinationsUrl,
    examination: examinationUrl,
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
    doctorByDepartmentUrl:function(departmentId){
        return doctorUrl+"?department="+departmentId
    }
}