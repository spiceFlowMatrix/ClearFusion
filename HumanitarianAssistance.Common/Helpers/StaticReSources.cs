using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Common.Helpers
{
    public static class StaticResource
    {

        // appsetting json file 
        public const string appsettingJsonFile = "appsettings.json";
        public const string googleCredential = "GoogleCredentials/";
        public const string googleCredentialNotFound = "Google Credentials not found";

        public const string EmailId = "hamza@edgsolutions.net";
        public const string ApplicationName = "OAuth client";
        public const string ProjectId = "clear-fusion-193608";
        public const string BucketName = "cf-staging-storage";
        public const string ProjectsFolderName = "Project-Management";
        public const string HRFolderName = "HR";
        public const string AccountingFolderName = "Accounting";
        public const string StoreFolderName = "Store";
        public const string ProjectActivityFolderName = "Activity";
        public const string uploadUrl = "https://storage.cloud.google.com/";
        public const string TrainingDocUrl = "https://drive.google.com/3r234?fa4r";

        public const string credentialsJsonFile = "credentials.json";

        public const int projectNotFound = 404;
        public const string projectNotFoundText = "Project not found";

        public const string invalidDate = "Actual date must be greater than or equal to start date";
        public const string validData = "Please enter valid data";
        public const int notValid = 501;

        public const int notFoundCode = 120;
        public const int successStatusCode = 200;
        public const int otherManagerAlreadyParticipatedStatusCode = 800;
        public const int failStatusCode = 400;
        public const int NotParticipatedCode = 100;
        public const int MandateNameAlreadyExistCode = 900;
        public const string NoDataFound = "No Data Found";
        public const int IdAlreadyUsedInOtherTable = 500;
        public const int NameAlreadyExist = 420;
        public const int ParticipationApprovedCode = 600;
        public const int ParticipationNotApprovedCode = 700;
        public const int ParticipationRejectionCode = 300;
        public const int AccountAlreadyExistsCode = 520;
        public const string ClaimAddedToRole = "Claim added to a role";
        public const string NoCompanyTypeFound = "No CompanyType Found";
        public const string NoCountryFound = "No Country found";
        public const string NoStateFound = "No State found";
        public const string InvalidCompanyTypeId = "Invalid Company type id";
        public const string InvalidRole = "Invalid Role";
        public const string SomethingWrong = "Something went wrong: ";
        public const string RoleCreated = "Role Created";
        public const string RoleAlreadyExist = "Role already exists";
        public const string InvalidUser = "Invalid User";
        public const string InvalidUserCredentials = "Invalid User credentials";
        public const string RoleAssignedToUser = "Role assigned to user";
        public const string CompanyCreated = "Company Account Created";
        public const string CompanyContactNotCreated = "Company contact not created";
        public const string CompanyContactAlreadyExists = "Company contact already exists";
        public const string CompanyAccountNotExists = "Company account not exists";
        public const string CompanyContactCreated = "Company contact created";
        public const string CompanyAccountNotCreated = "Company account not created";
        public const string CompanyAccountAlreadyExists = "Company account already exists";
        public const string CompanyNameAlreadyExists = "Company name already exists";
        public const string CompanyNameNotExists = "Company name not exists";
        public const string CompanyNameSameBySameId = "Company name saving by same Id";
        public const string RoleAlreadyAssignedToUser = "Role assigned to user";
        public const string EmailSentForResetPassword = "An email sent to registsred email address to reset password";
        public const string EmailNotSentForResetPassword = "Email not sent";
        public const string ChangePasswordSuccessfully = "Change Password successfully";
        public const string PasswordNotChange = "Password change failed";
        public const string PasswordResetSuccessfully = "Password reset successfuly";
        public const string PasswordResetNotSuccess = "Password not reset";
        public const string CompanyTypeAdded = "Company type added";
        public const string CompanyTypeUpdated = "Company type updated";
        public const string CompanyTypeDeleted = "Company type deleted";
        public const string CompanyTypeNotAdded = "Company Type Not Added";
        public const string CompanyTypeAlreadyExists = "Company Type already exists";
        public const string ComapnyDeleted = "Company has deleted successfully";
        public const string ComapnyNotDeleted = "Company has not deleted";
        public const string ComapnyContactDeleted = "Company contact has deleted";
        public const string ComapnyContactNotDeleted = "Company contact has not deleted";
        public const string CompanyDataNotFound = "Invalid Company";
        public const string CompanyContactNotFound = "Invalid Contact";
        public const string NoCompanyFound = "No Company Found";
        public const string NoCompanyContactFound = "No Company Contact Found";
        public const string CompanyUpdated = "Company details has been updated successfully";
        public const string CompanyContactUpdated = "Contact details has been updated successfully";
        public const string AccountStatusUpdated = "Account Status Updated";
        public const string InvalidCompanyId = "Invalid CompanyId";
        public const string AccountAlreadyExists = "Account Code already exists";
        public const string AccountCantAddToSameAccount = "Credit and Debit account are same !";
        public const string RecordNotFound = "Record not found";

        public const string unitRateExists = "Unit Rate already exists. Please select different combinations.";
        public const string TaskTypeAdded = "Task Type has added";
        public const string TaskTypeNotAdded = "Task Type has not added";
        public const string TaskTypeDeleted = "Task Type has deleted";
        public const string TaskTypeNotDeleted = "Task Type has not deleted";
        public const string InvalidTaskTypeId = "Invalid Task Type Id";
        public const string NoTaskTypeListFound = "No task type list found";
        public const string MasterTaskTypeUpdated = "Task Type has updated";
        public const string MasterTaskTypeNotUpdated = "Task Type has not updated";
        public const string TaskTypeAlreadyExist = "Task Type has already exists";
        public const string TaskCreated = "Task has created";
        public const string TaskNotCreated = "Task has not created";
        public const string TaskDeleted = "Task has deleted";
        public const string TaskNotDeleted = "Task has not deleted";
        public const string TaskEdit = "Task has updated";
        public const string TaskNotEdit = "Task has not updated";
        public const string InvalidTaskId = "Invalid Task Id";
        public const string NoTaskFound = "No Task has found";
        public const string GeographyAdded = "Geography has added";
        public const string GeographyNotAdded = "Geography has not added";
        public const string GeographyAlreadyExist = "Geography has already exists";
        public const string InvalidGeographyId = "Invalid GeographyId";
        public const string GeographyDeleted = "Geography has deleted";
        public const string GeographyNotDeleted = "Geography has not deleted";
        public const string GeographyEdit = "Geography has updated";
        public const string GeographyNotEdit = "Geography has not updated";
        public const string NoGeographyFound = "No Geography has found";
        public const string RoleDeleted = "Role has deleted";
        public const string RoleCannotDeleted = "Role cannot deleted";
        public const string RoleUpdated = "Role has updated";
        public const string RoleNotUpdated = "Role has not updated";
        //AssetsClass Resourse
        public const string AssetClassAdded = "Asset class has added";
        public const string AssetClassNotAdded = "Task Type has not added";
        public const string AssetClassAlreadyExist = "Asset class already exist";
        public const string InvalidAssetClassId = "Invalid AssetClassId";
        public const string AssetClassDeleted = "Asset Class has deleted";
        public const string AssetClassNotDeleted = "Asset Class has not deleted";
        public const string MasterAssetClassUpdated = "Asset Class has updated";
        public const string MasterAssetClassNotUpdated = "Asset Class has not updated";
        public const string NoAssetClassListFound = "No AssetClass list found";
        //ListName Resourse
        public const string ListNameAdded = "ListName has added";
        public const string ListNameNotAdded = "ListName has not added";
        public const string ListNameAlreadyExist = "Name already exist";
        public const string InvalidListNameId = "Invalid ListName Id";
        public const string ListNameDeleted = "ListName has deleted";
        public const string ListNameNotDeleted = "ListName has not deleted";
        public const string ListNameUpdated = "ListName has updated";
        public const string ListNameNotUpdated = "ListName has not updated";
        public const string NoListFound = "No list found";
        //Subscription Resourse
        public const string SubcriptionAdded = "Subcription Saved";
        public const string SubcriptionNotAdded = "Subcription not added";
        public const string SubcriptionAlreadyExist = "Subcription already exist";
        public const string InvalidSubcriptionId = "Invalid Subcription Id";
        public const string SubcriptionDeleted = "Subcription has deleted";
        public const string SubcriptionNotDeleted = "Subcription has not deleted";
        public const string SubcriptionUpdated = "Subcription has updated";
        public const string SubcriptionNotUpdated = "Subcription has not updated";
        public const string NoSubscriptionListFound = "No Subscription list found";

        // Mandate
        public const string MandateStageFound = "Mandate and Stage Found";
        public const string MandateStageNotFound = "Mandate and Stage Found";
        public const string MandateCreated = "Mandate has created";
        public const string MandateNotCreated = "Mandate has not created";
        public const string NoMandateFound = "No Mandate Found";
        public const string InvalidMandateId = "Invalid Mandate";
        public const string InvalidMandateStageId = "Invalid Mandate Stage";
        public const string NoStageFound = "No Stage Found";
        public const string MandateCompanySaved = "Mandate Company Saved";
        public const string InvalidData = "Invalid input data";
        public const string MandateTeamSaved = "Mandate Team Saved";
        public const string MandateStageTeamSaved = "Mandate Stage Team Saved";
        public const string MandateStageCompanySaved = "Mandate Stage Company Saved";
        public const string NoMandateStatusFound = "No Master Mandate Status Found";
        public const string MandateNameNotExists = "Mandate Name Not Exists";
        public const string MandateUpdated = "Mandate Updated";
        public const string MandateDeleted = "MandateDeleted";
        public const string MandateNotDeleted = "Mandate Cannot Deleted";
        public const string MandateNameAlreadyExist = "Name already exists";
        public const string MandateCopied = "Mandate copied";
        public const string MandateNotCopied = "Mandate Not copied";
        public const string StageNotUpdatedSuccessfully = "Stage not updated successfully";
        public const string NoNextStageFound = "No Next Stage Found";
        public const string StrategyTypeAdded = "Strategy Type Added";
        public const string StrategyTypeNotAdded = "Strategy Type Not Added";
        public const string StrategyTypeAlreadyExist = "Strategy Type Already exist";
        public const string InvalidStrategyID = "Invalid Strategy ID";
        //public const string StrategyUpdated = "Strategy Updated";
        // public const string StrategyNotUpdated = "Strategy Not Updated";

        // Workflow
        public const string WorkflowCreated = "Workflow has created";
        public const string WorkflowNotCreated = "Workflow has not created";
        public const string NoWorkFlowFound = "No WorkFlow Found";
        public const string InvalidWorkFlowId = "Invalid WorkFlow Id";
        public const string WorkFlowAlreadyLinked = "Cannot edit, WorkFlow Already Linked";
        public const string WorkFlowCannotDeleted = "Cannot Delete, WorkFlow Already Linked";
        public const string WorkFlowAlreadyExists = "WorkFlow name already exists";
        public const string WorkFlowDeleted = "Workflow deleted";
        public const string WorkFlowNotDeleted = "Workflow Not deleted";
        public const string WorkFlowCopied = "Mandate Copied";
        public const string WorkFlowNotCopied = "Mandate Not Copied";

        // Questionnaire
        public const string QuestionnaireCreated = "Questionnaire has created";
        public const string QuestionnaireNotCreated = "Questionnaire has not created";
        public const string NoQuestionnaireFound = "No Questionnaire Found";
        public const string QuestionnaireContactSaved = "Questionnaire Contact Saved";
        public const string QuestionnaireTeamSaved = "Questionnaire team Saved";
        public const string QuestionnaireSectionAdded = "Questionnaire Section Added";
        public const string InvalidQuestionnairePublishId = "Invalid Questionnaire PublishId";
        public const string QuestionnaireResponseSubmitted = "Questionnaire response submitted";
        public const string QuestionnaireResponseSaved = "Questionnaire response saved";
        public const string QuestionnaireResponseCannotSubmit = "Questionnaire response not submitted";
        public const string QuestionnaireResponseCannotSaved = "Questionnaire response not saved";
        public const string QuestionnaireFound = "Questionnaire found";
        public const string ParticipationSent = "Participation request sent";
        public const string ParticipationRejectedByManager = "Participation rejected by manager";
        public const string OtherManagerAlreadyParticipated = "Other manager from your company already participated";
        public const string QuestionnaireSectionDeleted = "Section Deleted";
        public const string QuestionnaireSectionNotDeleted = "Section Not Deleted";
        public const string AlreadyParticipated = "Already participated";
        public const string NotParticipated = "Not yet participated";
        public const string ParticipationNotApproved = "Participation not approved";
        public const string ParticipationApproved = "Participation approved";
        public const string ParticipatedRejected = "Participated rejected";
        public const string AllowClarificationUpdated = "Allow Clarification Updated";
        public const string NoResponseFound = "No querstionnaire response found";
        public const string QuestionnaireImportedSuccess = "Questionnaire Imported Successfully";
        public const string QuestionnaireNotImportedSuccess = "Questionnaire Not Imported Successfully";
        public const string PartiallyStatusUpdated = "Partially Status Updated";
        // public const string NoMasterQuestionnaireStatusFound = "No Master QuestionnaireStatus Found";

        public const string SectionOrSubsectionDeleted = "SectionOrSubsection Deleted";
        public const string InvalidSectionOrSubscetionId = "Invalid SectionOrSubsectionId";
        public const string SectionOrSubsectionUpdated = "SectionOrSubscetion Updated";
        public const string InvalidQuestionnaireId = "Invalid Questionnaire Id";
        //public const string InvalidSectionOrSubsectionId = "Invalid SectionOrSubsection";
        // public const string NoMasterQuestionnaireStatusFound = "No Master QuestionnaireStatus Found";

        //Question Type Choice
        public const string NoQuestionTypeChoiceFound = "No QuestionType Choice Found";

        //Feature
        public const string FeaturealreadyExists = "Feature already Exists";
        public const string FeatureAdded = "Feature added";
        public const string FeatureUpdated = "Feature updated";
        public const string FeatureNotAdded = "Feature not added";
        public const string NoFeatureFound = "No feature found";
        public const string InvalidFeatureId = "Invalid feature Id";
        public const string FeatureDeleted = "Feature has deleted";

        // Home site users
        public const string UserDetailsSaved = "Request has been saved. We will send you email once your account acitaved";
        public const string NoApplicationFound = "No Application Found";

        //Currency
        public const string NoCurrencyFound = "No Currency Found";

        //Company Contact
        public const string UserCreated = "New User Created";
        public const string UserAlreadyExist = "User is already exists";
        public const string UserNotExist = "User does not exists";
        public const string ApplicationStatusUpdated = "Application status has updated";
        public const string ContactAccountStatusUpdated = "Contact Account Status Updated";
        public const string InvalidContactId = "Invalid ContactId";
        public const string ContactSaved = "Client Contact Saved";

        //Question
        public const string QuestionCreated = "Question Created";
        public const string InvalidQuestionId = "Invalid QuestionId";
        public const string QuestionUpdated = "Question Updated";
        public const string QuestionDeleted = "Question Deleted";

        public const string QuestionnaireTeamRoleCreated = "Questionnaire Team Role Created";
        public const string QuestionnaireTeamRoleUpdated = "Questionnaire Team Role Updated";
        public const string DuplicateRole = "Duplicate Role";
        public const string QuestionnaireTeamRoleDeleted = "Questionnaire Team Role Deleted";
        public const string InvalidQuestionnaireTeamRoleId = "Invalid Questionnaire Team Role Id";


        //SubAssetClass
        public const string NoSubAssetClassFound = "No SubAsset Class Found";

        //Strategy
        public const string NoStrategyFound = "No Strategy Found";
        // Participant Query
        public const string QuerySaved = "Query Saved";
        public const string QueryResponseSaved = "Query Response Saved Successfully";
        public const string StrategyAdded = "Strategy Added";
        public const string NoStrategyAdded = "Strategy Not Added";
        public const string StrategyAlreadyExist = "Strategy already exist";
        public const string InvalidStrategyId = "Invalid Strategy Id";
        public const string StrategyUpdated = "Strategy Updated";
        public const string StrategyNotUpdated = "Strategy Not Updated";
        public const string StrategyRatingCreated = "Strategy Rating Created";
        public const string StrategyRatingUpdated = "Strategy Rating Updated";
        public const string StrategyRatingNotCreated = "Strategy Rating Not Created";
        public const string StrategyDeleted = "Strategy Deleted";
        public const string StrategyNotDeleted = "Strategy Not Deleted";
        public const string StrategyLinkedToMandate = "Strategy Linked To Mandate";
        //Delete MangerCompany
        public const string MandateCompanyDeleted = "MandateCompanyDeleted";
        public const string MandateCompanyNotDeleted = "MandateCompanyNotDeleted";

        //Document
        public const string NoDocumentTypeFound = "No Document Type Found";
        public const string DocumentFound = "Document Found";
        public const string DocumnetNotFound = "Document Not Found";
        public const string DocumentStatusUpdated = "Document Status Updated";
        public const string DocumentUploadSuccessfully = "Document Upload Successfully";


        //Delete Questionnaire
        public const string QuestionnaireDeleted = "QuestionnaireDeleted";
        public const string NoEvalutorFound = "No Evalutor Found";
        public const string NoReportFound = "No Report Found";
        public const string SomethingWentWrong = "Something Went Wrong";
        //
        public const string QuestionnaireDedlineDateNotPassed = "Questionnaire DedlineDate Not Passed";
        public const string SubAssetClassAdded = " Sub Asset class has added";
        public const string GeohraphyClassCreated = " Geohraphy Class Created";
        public const string GeographyLinkedToMandate = "Cannot deleted already linked to mandate";
        public const string AssetLinkedToMandate = "Cannot deleted already linked to mandate";
        public const string ShowWeightUpdated = "ShowWeightUpdated";

        //Notification
        public const string NoticiationNotFound = "Notification Not Found";
        public const string NotificationUpdated = "Notification Updated";
        public const string NotificationNotUpdated = "Notification Not Updated";
        public const string InvalidId = "Invalid Id";
        // Notification Messages
        public const string Director = "You have been assigned as a Director role for questionnaire";
        public const string Viewer = "You have been assigned as a Viewer role for questionnaire";
        public const string Publisher = "You have been assigned as a Publisher role for questionnaire";
        public const string Writer = "You have been assigned as a Writer role for questionnaire";
        public const string Evaluator = "You have been assigned as a Evaluator role for questionnaire";

        //Review Template
        public const string TemplateCreated = "Template Created";
        public const string TemplateUpdated = "Template Updated";

        //Store
        public const string DeleteProcurementsFirst = "Please Delete Procurements First";
        public const string AccountNoteNotExists = "Account doesn't exists";
        public const string InventoryCodeAlreadyExists = "Inventory Code already exists";

        //ExchangeRate
        public const string ExchagneRateNotDefined = "Exchange rate is not defined !";

        public const string HoursAlreadySet = "Hours are already assigned for this Month";
        public const string CannotAddAdvance = "Cannot Add Advance for {0}-{1} as an uncleared Advance already exists";

        //HR
        public const string PensionPaymentCreated = "Pension Payment Done On {0} for {1}";
        public const string NoAttendanceToAdd ="No Attendance to Add";
        public const string PensionPayment = "Pension Payment";
        // Marketing
        public const string unitRateNotFound = "Unit Rate does not exists. Please try other combinations..";

        //Salary Payment
        public const string SalaryPaymentDone = "Salary Payment: "; //EmpCode-EmpName-Month-<salary payment>
        public const string SalaryHeadAllowances = "{0} has been debited towards Gross Salary";
        public const string SalaryHeadDeductions = "{0} has been credited towards Net Salary";
        public const string JobCodeExist = "Job Code Already Exist";
        public const string PurchaseVoucherCreated = "Purchase Voucher Created";
        public const string PurchaseVoucherUnverified = "Reverse Purchase Payments";
        public const string NoPension = "Pension Record Not Found";

        public const string NameExist = "Name Already Exist";
        public const string SuccessText = "Success";
        public const string FileText = "File Not Supported";
        public const int FileNotSupported = 4440;

        #region "Project"
        public const string ActivityNotFound = "Activity Not Found";
        public const string ProjectIndicatorNotFound = "Project Indicator Record Not Found";
        public const string IndicatorNameEmpty = "Project Indicator Name Can Not Be Empty";
        #endregion

        #region "Accounting New"
        public const string ExceedLevelCount = "You have reached Maximum Limit";
        public const string ParentIdNotPresent = "Account does not exist";
        public const string VoucherNotPresent = "Voucher does not exist";

        public const string VoucherVerified = "Voucher is verified";
        public const string VoucherUnVerified = "Voucher is Unverified";
        public const string VoucherNotSaved = "Unable to Create Voucher";
        public const string CurrencyNotFound = "Currency not found";
        public const string defaultFinancialYearIsNotSet = "Default Financial year is not set";
        public const string FinancialYearAlreadyExists = "Financial Year Already exists!";
        public const string FinancialYearNotFound = "Financial Year not found";
        public const string officeCodeNotFound = "Office Code Not Found";
        public const string TransactionNotFound = "Transaction Not Found";
        public const string AccountNotFound = "Account Not Found";
        public const string DeleteAllTransactions = "Transactions Exist";
        public const string DeleteAllChildAccount = "Child Account Exist";
        public const string TransactionsNotSaved = "Transactions Not Saved";

        #endregion

        #region "Exception Throw Error Text"
        public const string UnableToUploadFile = "Unable To Upload File";
        public const string ActivityExtensionNotFound = "Activity extension not found";

        #endregion

        public const string UnableToGenerateSignedUrl = "Unable to generate signed Url. Try Again!";
        public const string BucketNameNotFound = "Bucket name not found.";
        public const string UnableToDeleteBucketObject = "Unable to delete file. Try Again!";


        public const string OpportunityControlNotfound = "Opportunity Control not found";
        public const string LogisticsControlNotfound = "Logistics Control not found";
        public const string ActivitiesControlNotfound = "Activities Control not found";
        public const string HiringControlNotfound = "Hiring Control not found";

        public const string sameRoleAlreadyExistForTheUser = "Same Role already exist for this user";
        public const string NoTransactionToUpDate = "No Transaction To UpDate";
        public const string JournalNotFound = "Journal not found";

        #region Chat
        public const string ChatMessageEmpty = "Message cannot be empty";
        public const string ChatNotFound = "Could not find chat to edit";
        #endregion

    }
}
