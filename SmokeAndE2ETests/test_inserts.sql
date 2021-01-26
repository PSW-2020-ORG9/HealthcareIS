insert into user.Countries(Id,Name,Code) values (1,"Serbia","+381");
insert into user.Cities(Id,Name,PostalCode,CountryId) values (1,"Novi Sad","21000",1);
insert into user.Cities(Id,Name,PostalCode,CountryId) values (2,"Belgrade","11000",1);

insert into user.Countries(Id,Name,Code) values (2,"Montenegro","+381");
insert into user.Cities(Id,Name,PostalCode,CountryId) values (3,"Budva","5000",2);
insert into user.Cities(Id,Name,PostalCode,CountryId) values (4,"Podgorica","5100",2);

insert into user.Persons (Id,Name,Surname,DateOfBirth,Address,TelephoneNumber,MiddleName,Age,MaritalStatus,Gender,CityOfResidenceId,CityOfBirthId)
values (1,'Slobodan','Jovanovic','1996-10-10','Joakima Vujica 33','012234344','Marko',25,'Married','Male',1,1);
insert into user.Patients (Id,InsuranceNumber,Status,PersonId) values (1,'1234','Alive',1);
insert into user.UserAccounts (Id,Credentials_Username,Credentials_Password,Credentials_Email,
PatientId,RespondedToSurvey,IsActivated,UserGuid,Discriminator,Role,AvatarUrl)
 values (1,'sloba','boba','bobo@bobomail.com',1,1,1,uuid(),'PatientAccounts','Patient','https://i.imgur.com/pXS6agt.jpg');
 
insert into user.Persons (Id,Name,Surname,DateOfBirth,Address,TelephoneNumber,MiddleName,Age,MaritalStatus,Gender,CityOfResidenceId,CityOfBirthId)
values (2,'Mirko','Jovanovic','1996-10-10','Joakima Vujica 33','012234344','Marko',25,'Married','Male',1,1);
 insert into user.Patients (Id,Status,PersonId) values (2,"Alive",2);
 insert into user.UserAccounts (Id,Credentials_Username,Credentials_Password,Credentials_Email,
PatientId,RespondedToSurvey,IsActivated,UserGuid,Discriminator,Role,AvatarUrl)
 values (2,'mire','mirekralj','bobo@bobomail.com',2,1,1,uuid(),'PatientAccounts','Patient','https://i.imgur.com/LEQnTi6.jpg');
 
 
insert into user.Persons (Id,Name,Surname,DateOfBirth,Address,TelephoneNumber,MiddleName,Age,MaritalStatus,Gender,CityOfResidenceId,CityOfBirthId)
values (3,'Nikola','Nikolic','1996-10-10','Joakima Vujica 33','012234344','Marko',25,'Married','Male',1,1);
insert into user.Patients (Id,Status,PersonId) values (3,"Alive",3);
insert into user.UserAccounts (Id,Credentials_Username,Credentials_Password,Credentials_Email,
PatientId,RespondedToSurvey,IsActivated,UserGuid,Discriminator,Role,AvatarUrl)
values (3,'nikola','123456','nikola@mail.com',3,1,1,uuid(),'PatientAccount','Patient','https://i.imgur.com/zcGtCQt.jpg');

insert into user.Persons (Id,Name,Surname,DateOfBirth,Address,TelephoneNumber,MiddleName,Age,MaritalStatus,Gender,CityOfResidenceId,CityOfBirthId)
values (4,'Borko','Borkovic','1996-10-10','Zmaj Jovina 20','012234344','Borko',25,'Married','Male',1,1);
insert into user.Patients (Id,Status,PersonId) values (4,"Alive",4);
insert into user.UserAccounts (Id,Credentials_Username,Credentials_Password,Credentials_Email,
PatientId,RespondedToSurvey,IsActivated,UserGuid,Discriminator,Role,AvatarUrl)
values (4,'borko','123456','admin@mail.com',4,1,1,uuid(),'UserAccount','Admin','https://i.imgur.com/Iik4Wcl.jpg');


insert into user.Departments(Id,Name) values (1,"Dermatologija");
insert into user.Departments(Id,Name) values (2,"Oftamologija");
insert Into user.Persons(Id,Name,Surname,DateOfBirth,MaritalStatus,CityOfResidenceId,CityOfBirthId,Age,Gender)
values("123","Milos","Milosevic",'1973-01-01',"Married",1,1,20,'Male');
insert Into user.Persons(Id,Name,Surname,DateOfBirth,MaritalStatus,CityOfResidenceId,CityOfBirthId,Age,Gender)
values("443","Mirko","Milosevic",'1973-01-01',"Married",1,1,20,'Male');

insert into user.Doctors(Id,PersonId,Status,DepartmentId) values(1,"123","Current",1);
insert into user.Doctors(Id,PersonId,Status,DepartmentId) values(2,"443","Current",2);
insert into user.Specialties(id, name, description) values (1, "Doktor opste prakse", "Lekar za osnovne potrebe.");
insert into user.DoctorSpecialties(specialtyid, doctorid) values(1, 1);
insert into user.Specialties(id, name, description) values (2, "Dermatolog", "Lekar za kožu.");
insert into user.DoctorSpecialties(specialtyid, doctorid) values(2,2);


insert into hospital.Departments(Id,Name) values (1,"Dermatologija");
insert into hospital.Departments(Id,Name) values (2,"Oftamologija");
insert into hospital.Rooms(id, name, purpose, departmentid) values(1, "Soba 1", "Soba za odmor", 1);
insert into hospital.Rooms(id, name, purpose, departmentid) values(2, "Soba 2", "Soba za operacije", 1);



insert into schedule.Shifts(Id,TimeInterval_Start,TimeInterval_End,AssignedExamRoomId,DoctorId)
values (1,'2021-01-26 8:00:00','2021-01-26 14:00:00',1,1);
insert into schedule.Shifts(Id,TimeInterval_Start,TimeInterval_End,AssignedExamRoomId,DoctorId)
values (2,'2021-01-28 8:00:00','2021-01-28 10:00:00',1,1);
insert into schedule.Shifts(Id,TimeInterval_Start,TimeInterval_End,AssignedExamRoomId,DoctorId)
values (3,'2021-01-31 8:00:00','2021-01-31 10:00:00',1,1);
insert into schedule.Shifts(Id,TimeInterval_Start,TimeInterval_End,AssignedExamRoomId,DoctorId)
values (4,'2021-02-02 8:00:00','2021-02-02 10:00:00',1,1);

insert into schedule.Shifts(Id,TimeInterval_Start,TimeInterval_End,AssignedExamRoomId,DoctorId)
values (5,'2021-01-27 13:00:00','2021-01-27 19:00:00',2,2);
insert into schedule.Shifts(Id,TimeInterval_Start,TimeInterval_End,AssignedExamRoomId,DoctorId)
values (6,'2021-01-29 13:00:00','2021-01-27 18:00:00',2,2);
insert into schedule.Shifts(Id,TimeInterval_Start,TimeInterval_End,AssignedExamRoomId,DoctorId)
values (7,'2021-02-01 15:00:00','2021-02-01 19:00:00',2,2);

insert into feedback.UserFeedbacks (Id,Date,UserComment,FeedbackVisibility_IsPublic,FeedbackVisibility_IsAnonymous,FeedbackVisibility_IsPublished,PatientAccountId)
values (1,'2020-12-15 9:40:00','Dobra usluga, sve preporuke.',1,1,1,1);
insert into feedback.UserFeedbacks (Id,Date,UserComment,FeedbackVisibility_IsPublic,FeedbackVisibility_IsAnonymous,FeedbackVisibility_IsPublished,PatientAccountId)
values (2,'2020-12-19 13:23:00','Prijatno osoblje klinike.',1,1,1,2);
insert into feedback.UserFeedbacks (Id,Date,UserComment,FeedbackVisibility_IsPublic,FeedbackVisibility_IsAnonymous,FeedbackVisibility_IsPublished,PatientAccountId)
values (3,'2020-10-13 17:32:00','Najbolja klinika na svetu!!!',1,1,0,2);

insert into schedule.Examinations (Id,TimeInterval_Start,TimeInterval_End,DoctorId,PatientId,RoomId,Priority,IsCanceled,RequiredSpecialtyId)
values (1,'2020-12-15 9:40:00','2020-12-15 10:10:00',1,3,1,'Low',0,1);
insert into schedule.Examinations (Id,TimeInterval_Start,TimeInterval_End,DoctorId,PatientId,RoomId,Priority,IsCanceled,RequiredSpecialtyId)
values (2,'2020-12-10 13:40:00','2020-12-10 14:10:00',1,3,1,'Low',0,1);
insert into schedule.Examinations (Id,TimeInterval_Start,TimeInterval_End,DoctorId,PatientId,RoomId,Priority,IsCanceled,RequiredSpecialtyId)
values (3,'2020-09-11 20:20:00','2020-09-11 20:50:00',1,3,1,'Low',0,1);


insert into schedule.ExaminationReports (Id,Anamnesis) values (1,'Osip u predelu prepona');
insert into schedule.Diagnoses(Id,Name,Description,IsInjury,ExaminationReportId) values
(1,'MKB-10 R21 Koprivnjača i drugi osip kože','Osip nastao kao posledica celodnevnog nošenja kostima od lateksa.',1,1);
update schedule.Examinations set ExaminationReportId=1 where Id=1;
insert into hospital.Medications (Id,Name,Manufacturer,Description,Type) values
(1,'Hiper JEKO ZN','Hemopharm','Preparat protiv ojeda','Topical');
insert into hospital.IntakeInstructions(Id,StartDate,EndDate,TimesPerDay,Dosage,DosageUnit,Description)
values (1,'2020-12-15 10:10:00','2020-12-22 10:10:00',1,1,'Namaz','Mazati po malo na mesto ojeda jedanput dnevno');
insert into hospital.MedicationPrescriptions (Id,ExaminationReportId,DiagnosisId,MedicationId,MedicalRecordId,InstructionsId)
values (1,1,1,1,1,1);

insert into schedule.ExaminationReports (Id,Anamnesis) values (2,'Lakše opekotine');
insert into schedule.Diagnoses(Id,Name,Description,IsInjury,ExaminationReportId) values
(2,'MKB-10 T23.3 Opekotina ručja i šake prvog stepena','Lakše opekotine nastale nespretnim rukovanjem sa oranijom u kojoj su se kuvali čvarci',1,2);
update schedule.Examinations set ExaminationReportId=2 where Id=2;
insert into hospital.Medications (Id,Name,Manufacturer,Description,Type) values
(2,'Jekoderm','Galenika','Preparat protiv osipa','Topical');
insert into hospital.IntakeInstructions(Id,StartDate,EndDate,TimesPerDay,Dosage,DosageUnit,Description)
values (2,'2020-12-10 14:10:00','2020-12-20 14:10:00',1,1,'Namaz','Mazati po malo na mesto opekotine jedanput dnevno');
insert into hospital.MedicationPrescriptions (Id,ExaminationReportId,DiagnosisId,MedicationId,MedicalRecordId,InstructionsId)
values (2,2,2,2,2,2);

insert into schedule.ExaminationReports (Id,Anamnesis) values (3,'Acne vulgaris');
insert into schedule.Diagnoses(Id,Name,Description,IsInjury,ExaminationReportId) values
(3,'MKB-10 L770.0 Obične akne','Akne u predelu lica i vrata',1,3);
update schedule.Examinations set ExaminationReportId=3 where Id=3;
insert into hospital.Medications (Id,Name,Manufacturer,Description,Type) values
(3,'ECOMER','NATUMIN PHARMA','Preparat protiv akni','Capsule');
insert into hospital.IntakeInstructions(Id,StartDate,EndDate,TimesPerDay,Dosage,DosageUnit,Description)
values (3,'2020-09-11 20:50:00','2020-10-11 20:50:00',1,1,'Kapsula','1x dnevno, posle rucka, u narednih mesec dana.');
insert into hospital.MedicationPrescriptions (Id,ExaminationReportId,DiagnosisId,MedicationId,MedicalRecordId,InstructionsId)
values (3,3,3,3,3,3);


insert into schedule.Examinations (Id,TimeInterval_Start,TimeInterval_End,DoctorId,PatientId,RoomId,Priority,IsCanceled,RequiredSpecialtyId)
values (4,'2021-01-25 21:10:00','2021-01-25 21:40:00',2,3,2,'Low',0,1);
insert into schedule.Examinations (Id,TimeInterval_Start,TimeInterval_End,DoctorId,PatientId,RoomId,Priority,IsCanceled,RequiredSpecialtyId)
values (5,'2021-01-27 16:30:00','2021-01-27 17:00:00',2,3,2,'Low',0,1);
insert into schedule.Examinations (Id,TimeInterval_Start,TimeInterval_End,DoctorId,PatientId,RoomId,Priority,IsCanceled,RequiredSpecialtyId)
values (6,'2021-01-30 11:30:00','2021-01-30 12:00:00',1,3,1,'Low',0,1);
insert into schedule.Examinations (Id,TimeInterval_Start,TimeInterval_End,DoctorId,PatientId,RoomId,Priority,IsCanceled,RequiredSpecialtyId)
values (7,'2021-02-05 09:30:00','2021-02-05 10:00:00',1,3,1,'Low',0,1);


insert into feedback.Surveys (Id) value (1);
insert into feedback.SurveySections (Id,SectionName,SurveyId,IsDoctorSection)
values(1,"Medical center",1,False);
insert into feedback.SurveySections (Id,SectionName,SurveyId,IsDoctorSection)
values(2,"Our medical staff",1,False);
insert into feedback.SurveySections (Id,SectionName,SurveyId,IsDoctorSection)
values(3,"About doctor",1,True);

insert into feedback.SurveyQuestions (Id,Question,SurveySectionId) 
values (1,"How likely would you be to recommend us to a friend or colleague",1);
insert into feedback.SurveyQuestions (Id,Question,SurveySectionId) 
values (2,"How would you rate hygiene at the medical center",1);
insert into feedback.SurveyQuestions (Id,Question,SurveySectionId) 
values (3,"How would you rate professionalism of our staff",2);
insert into feedback.SurveyQuestions (Id,Question,SurveySectionId) 
values (4,"The medical staff is friendly and make me feel appreciated",2);
insert into feedback.SurveyQuestions (Id,Question,SurveySectionId) 
values (5,"The doctor is welcoming and gentle",3);
insert into feedback.SurveyQuestions (Id,Question,SurveySectionId) 
values (6," The doctor listened and understood my medical concerns",3);

------------------------
insert into feedback.SurveyResponses (Id,SubmittedAt,PatientAccountId,SurveyId,ExaminationId)
values (1,"2020-12-15",1,1,1); 
insert into feedback.RatedSurveySections(Id,SurveySectionId,SurveyResponseId,Discriminator)
values(1,1,1,"RatedSurveySection");
insert into feedback.RatedSurveySections(Id,SurveySectionId,SurveyResponseId,Discriminator)
values(2,2,1,"RatedSurveySection");
insert into feedback.RatedSurveySections(Id,SurveySectionId,SurveyResponseId,Discriminator,DoctorId)
values(3,3,1,"DoctorSurveySection",1);

update feedback.SurveyResponses 
set DoctorSurveySectionId=3
where Id=1;

insert into feedback.RatedSurveyQuestions(Id,SurveyQuestionId,rating,RatedSurveySectionId)
values (1,1,4,1);
insert into feedback.RatedSurveyQuestions(Id,SurveyQuestionId,rating,RatedSurveySectionId)
values (2,2,3,1);
insert into feedback.RatedSurveyQuestions(Id,SurveyQuestionId,rating,RatedSurveySectionId)
values (3,3,5,2);
insert into feedback.RatedSurveyQuestions(Id,SurveyQuestionId,rating,RatedSurveySectionId)
values (4,4,5,2);
insert into feedback.RatedSurveyQuestions(Id,SurveyQuestionId,rating,RatedSurveySectionId)
values (5,5,2,3);
insert into feedback.RatedSurveyQuestions(Id,SurveyQuestionId,rating,RatedSurveySectionId)
values (6,6,4,3);

-------------------------------
insert into feedback.SurveyResponses (Id,SubmittedAt,PatientAccountId,SurveyId,ExaminationId)
values (2,"2020-10-10",1,1,3); 
insert into feedback.RatedSurveySections(Id,SurveySectionId,SurveyResponseId,Discriminator)
values(4,1,1,"RatedSurveySection");
insert into feedback.RatedSurveySections(Id,SurveySectionId,SurveyResponseId,Discriminator)
values(5,2,1,"RatedSurveySection");
insert into feedback.RatedSurveySections(Id,SurveySectionId,SurveyResponseId,Discriminator,DoctorId)
values(6,3,1,"DoctorSurveySection",2);

update feedback.SurveyResponses 
set DoctorSurveySectionId=3
where Id=6;

insert into feedback.RatedSurveyQuestions(Id,SurveyQuestionId,rating,RatedSurveySectionId)
values (7,1,5,4);
insert into feedback.RatedSurveyQuestions(Id,SurveyQuestionId,rating,RatedSurveySectionId)
values (8,2,4,4);
insert into feedback.RatedSurveyQuestions(Id,SurveyQuestionId,rating,RatedSurveySectionId)
values (9,3,5,5);
insert into feedback.RatedSurveyQuestions(Id,SurveyQuestionId,rating,RatedSurveySectionId)
values (10,4,4,5);
insert into feedback.RatedSurveyQuestions(Id,SurveyQuestionId,rating,RatedSurveySectionId)
values (11,5,3,6);
insert into feedback.RatedSurveyQuestions(Id,SurveyQuestionId,rating,RatedSurveySectionId)
values (12,6,5,6);

---------------------------------------------


insert into feedback.SurveyResponses (Id,SubmittedAt,PatientAccountId,SurveyId,ExaminationId)
values (3,"2020-03-04",2,1,2); 
insert into feedback.RatedSurveySections(Id,SurveySectionId,SurveyResponseId,Discriminator)
values(7,1,3,"RatedSurveySection");
insert into feedback.RatedSurveySections(Id,SurveySectionId,SurveyResponseId,Discriminator)
values(8,2,3,"RatedSurveySection");
insert into feedback.RatedSurveySections(Id,SurveySectionId,SurveyResponseId,Discriminator,DoctorId)
values(9,3,3,"DoctorSurveySection",2);

update feedback.SurveyResponses 
set DoctorSurveySectionId=9
where Id=3;

insert into feedback.RatedSurveyQuestions(Id,SurveyQuestionId,rating,RatedSurveySectionId)
values (13,1,3,7);
insert into feedback.RatedSurveyQuestions(Id,SurveyQuestionId,rating,RatedSurveySectionId)
values (14,2,4,7);
insert into feedback.RatedSurveyQuestions(Id,SurveyQuestionId,rating,RatedSurveySectionId)
values (15,3,5,8);
insert into feedback.RatedSurveyQuestions(Id,SurveyQuestionId,rating,RatedSurveySectionId)
values (16,4,3,8);
insert into feedback.RatedSurveyQuestions(Id,SurveyQuestionId,rating,RatedSurveySectionId)
values (17,5,5,9);
insert into feedback.RatedSurveyQuestions(Id,SurveyQuestionId,rating,RatedSurveySectionId)
values (18,6,4,9);

-- events


SELECT * FROM es.SchedulingEvents;

insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (1,'2010-01-01 10:10:10',0,'2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6',18,1);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (2,'2010-01-01 10:10:13',1,'2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6',18,1);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (3,'2010-01-01 10:10:16',2,'2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6',18,1);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (4,'2010-01-01 10:10:16',3,'2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6',18,1);

insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (5,'2010-02-02 10:10:10',0,'46f62173-f7de-4668-9492-627fa4ee6070',25,2);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (6,'2010-02-02 10:10:16',1,'46f62173-f7de-4668-9492-627fa4ee6070',25,2);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (7,'2010-02-02 10:10:20',2,'46f62173-f7de-4668-9492-627fa4ee6070',25,2);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (8,'2010-02-02 10:10:23',1,'46f62173-f7de-4668-9492-627fa4ee6070',25,2);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (9,'2010-02-02 10:10:30',2,'46f62173-f7de-4668-9492-627fa4ee6070',25,2);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (10,'2010-02-02 10:10:40',3,'46f62173-f7de-4668-9492-627fa4ee6070',25,2);

insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (11,'2010-02-02 10:10:10',0,'0a9e870c-73a1-421d-8a73-d02bcd6e5e09',40,3);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (12,'2010-02-02 10:10:16',1,'0a9e870c-73a1-421d-8a73-d02bcd6e5e09',40,3);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (13,'2010-02-02 10:10:20',2,'0a9e870c-73a1-421d-8a73-d02bcd6e5e09',40,3);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (14,'2010-02-02 10:10:23',1,'0a9e870c-73a1-421d-8a73-d02bcd6e5e09',40,3);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (15,'2010-02-02 10:10:30',2,'0a9e870c-73a1-421d-8a73-d02bcd6e5e09',40,3);

insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (16,'2010-02-02 10:10:10',0,'b73d0e9d-9abb-4502-9208-34eaafa5283a',45,4);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (17,'2010-02-02 10:10:16',1,'b73d0e9d-9abb-4502-9208-34eaafa5283a',45,4);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (18,'2010-02-02 10:10:26',2,'b73d0e9d-9abb-4502-9208-34eaafa5283a',45,4);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (19,'2010-02-02 10:10:27',1,'b73d0e9d-9abb-4502-9208-34eaafa5283a',45,4);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (20,'2010-02-02 10:10:29',0,'b73d0e9d-9abb-4502-9208-34eaafa5283a',45,4);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (21,'2010-02-02 10:10:31',1,'b73d0e9d-9abb-4502-9208-34eaafa5283a',45,4);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (22,'2010-02-02 10:10:33',2,'b73d0e9d-9abb-4502-9208-34eaafa5283a',45,4);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (23,'2010-02-02 10:10:38',3,'b73d0e9d-9abb-4502-9208-34eaafa5283a',45,4);


insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (24,'2010-02-02 10:10:10',0,'b443e4b0-b1f7-46fc-b1db-5e0c34d3a797',33,5);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (25,'2010-02-02 10:10:16',1,'b443e4b0-b1f7-46fc-b1db-5e0c34d3a797',33,5);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (26,'2010-02-02 10:10:20',2,'b443e4b0-b1f7-46fc-b1db-5e0c34d3a797',33,5);

insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (27,'2010-02-02 10:10:10',0,'a0540dbf-5cb5-40f3-bb38-1ac34d084802',72,6);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (28,'2010-02-02 10:10:20',1,'a0540dbf-5cb5-40f3-bb38-1ac34d084802',72,6);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (29,'2010-02-02 10:10:30',2,'a0540dbf-5cb5-40f3-bb38-1ac34d084802',72,6);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (30,'2010-02-02 10:10:35',3,'a0540dbf-5cb5-40f3-bb38-1ac34d084802',72,6);

insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (31,'2010-02-02 10:10:10',0,'0d9f367a-8b96-4440-80bf-922a84eb375e',35,7);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (32,'2010-02-02 10:10:21',1,'0d9f367a-8b96-4440-80bf-922a84eb375e',35,7);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (33,'2010-02-02 10:10:24',2,'0d9f367a-8b96-4440-80bf-922a84eb375e',35,7);
insert into es.SchedulingEvents (Id,TimeStamp,EventType,SchedulingSessionId,UserAge,UserId)
values (34,'2010-02-02 10:10:25',3,'0d9f367a-8b96-4440-80bf-922a84eb375e',35,7);













