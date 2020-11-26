let server='http://localhost:5000'
let examinationsUrl = server + '/docsearch/examination'
let prescriptionsUrl = server + '/docsearch/prescription'
let feedbacksUrl = server + '/feedback'
let patientUrl =   server + '/patient'
let surveyPreviewUrl = server+'/survey/preview/admin/1'

export default{
    feedback: feedbacksUrl ,
    patient: patientUrl,
    surveyPreview: surveyPreviewUrl,
    
    examinations: examinationsUrl,
    prescriptions: prescriptionsUrl,

    docSearchPrescriptionSimple: prescriptionsUrl + '/simple',
    docSearchExaminationSimple: examinationsUrl + '/simple',
}