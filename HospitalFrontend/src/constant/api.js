let server='http://localhost:5000'
let examinationsUrl = server + '/docsearch/examination'
let prescriptionsUrl = server + '/docsearch/prescription'

export default{
<<<<<<< HEAD
    feedback:server+'/feedback',
    surveyPreview:server+'/survey/preview/admin/1'
=======
    feedback: server + '/feedback',
    patient: server + '/patient',
    
    examinations: examinationsUrl,
    prescriptions: prescriptionsUrl,

    docSearchPrescriptionSimple: prescriptionsUrl + '/simple',
    docSearchExaminationSimple: examinationsUrl + '/simple',
>>>>>>> master
}