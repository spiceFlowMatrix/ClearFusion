export interface IInterviewTechnicalQuestionModel {
  TechnicalQuestionId: number;
  Question: any;
  Answer: string;
}

export interface ISkillRatingModel {
  SkillRatingId: number;
  SkillRatingName: string;
}

export interface IInterviewLanguagesModel {
  LanguageName: string;
  LanguageId: number;
  Reading: number;
  Writing: number;
  Listening: number;
  Speaking: number;
}

export interface IInterviewTrainingModel {
  TraininigType: any;
  TrainingName: any;
  StudyingCountry: any;
  StartDate: any;
  EndDate: any;
}
export interface IEmployeeListModel {
  EmployeeId: any;
  EmployeeName: any;
  EmployeeCode: any;
  CodeEmployeeName: any;
}

// Main Form
export interface IEmpInterviewFormModel {
  InterviewDetailsId: number;
  EmployeeID: number;
  Qualification: string;
  DutyStation: number;
  JobId: number;
  University: string;
  PassportNo: string;
  PlaceOfBirth: any;
  TazkiraIssuePlace: string;
  MaritalStatus: any;

  Experience: string;
  RatingBasedCriteriaModelList: any[];

  ProfessionalCriteriaMarks: any;

  MarksObtained: any;
  WrittenTestMarks: any;
  Ques1: any;
  Ques2: any;
  Ques3: any;
  PreferedLocation: any;
  NoticePeriod: any;
  JoiningDate: any;

  InterviewLanguageModelList: IInterviewLanguagesModel[];
  InterviewTrainingModelList: IInterviewTrainingModel[];
  InterviewTechQuesModelList: IInterviewTechnicalQuestionModel[];

  // Compensation
  CurrentBase: any;
  CurrentTransportation: any;
  CurrentMeal: any;
  CurrentOther: any;
  ExpectationBase: any;
  ExpectationTransportation: any;
  ExpectationMeal: any;
  ExpectationOther: any;

  TotalMarksObtained: any;

  // Recommendation
  Status: number;
  Interviewers: any[];

  InterviewStatus: string;
}

//#region "classes"

export interface IEmpExitInterviewFormModel {
  ExistInterviewDetailsId: number;
  EmployeeId: number;

  EmployeeCode: string;
  EmployeeName: string;
  Position: any;
  Department: any;
  TenureWithCHA: any;
  Gender: any;
  OfficeId: any;

  // FeelingAboutEmployee
  DutiesOfJob: any;
  TrainingAndDevelopmentPrograms: any;
  OpportunityAdvancement: any;
  SalaryTreatment: any;
  BenefitProgram: any;
  WorkingConditions: any;
  WorkingHours: any;
  CoWorkers: any;
  Supervisors: any;
  GenderFriendlyEnvironment: any;
  OverallJobSatisfaction: any;

  // ReasonOfLeaving
  Benefits: any;
  BetterJobOpportunity: any;
  FamilyReasons: any;
  NotChallenged: any;
  Pay: any;
  PersonalReasons: any;
  Relocation: any;
  ReturnToSchool: any;
  ConflictWithSuoervisors: any;
  ConflictWithOther: any;
  WorkRelationship: any;
  CompanyInstability: any;
  CareerChange: any;
  HealthIssue: any;

  // TheDepartment
  HadGoodSynergy: any;
  HadAdequateEquipment: any;
  WasAdequatelyStaffed: any;
  WasEfficient: any;

  // TheJobItself
  JobWasChallenging: any;
  SkillsEffectivelyUsed: any;
  JobOrientation: any;
  WorkLoadReasonable: any;
  SufficientResources: any;
  WorkEnvironment: any;
  ComfortableAppropriately: any;
  Equipped: any;

  // MySupervisor
  HadKnowledgeOfJob: any;
  HadKnowledgeSupervision: any;
  WasOpenSuggestions: any;
  RecognizedEmployeesContribution: any;

  // TheManagement
  GaveFairTreatment: any;
  WasAvailableToDiscuss: any;
  WelcomedSuggestions: any;
  MaintainedConsistent: any;
  ProvidedRecognition: any;
  EncouragedCooperation: any;
  ProvidedDevelopment: any;

  Question: any;
  Explain: string;
}

export interface IEmployeeMoreDetailModel {
  EmployeeId: any;
  EmployeeName: any;
  EmployeeCode: any;
  FathersName: any;
  Position: any;
  Department: any;
  Qualification: any;
  DutyStation: any;
  RecruitmentDate: any;
  TenureWithCHA: any;
  Gender: any;
}
