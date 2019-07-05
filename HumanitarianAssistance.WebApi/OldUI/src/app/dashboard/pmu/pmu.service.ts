import { Injectable } from '@angular/core';

// PMU Projects starts
export class PMUProject {
  ID: number;
  Sector: string;
  ProjectCode: string;
  ProjectName: string;
  ProjectBudget: number;
  Donor: string;
}

const pmuprojects: PMUProject[] = [
  {
    ID: 1,
    Sector: 'Rural Development & Social Protection',
    ProjectCode: '1234',
    ProjectName: 'Welfare',
    ProjectBudget: 9000,
    Donor: 'test'
  },
  {
    ID: 2,
    Sector: 'Urban HealthCare',
    ProjectCode: '5678',
    ProjectName: 'Xenon',
    ProjectBudget: 12000,
    Donor: 'test'
  },
  {
    ID: 3,
    Sector: 'Accounting',
    ProjectCode: '2244',
    ProjectName: 'Amada',
    ProjectBudget: 10000,
    Donor: 'test'
  }
];

// PMU Projects ends

// PMU Projects Add PopupClass starts
export class PMUProjectDetail {
  ID: number;
  Project: string;
  MeasureType: string;
  OpportunityNoType: string;
  OpportunityId: string;
  Province: string;
  District: string;
  Office: string;
  PMUSector: string;
  StartDate: string;
  EndDate: string;
  CurrencyType: string;
  Budget: string;
  DirectBeneficiaryMale: string;
  DirectBeneficiaryFemale: string;
  IndirectBeneficiaryMale: string;
  IndirectBeneficiaryFemale: string;
  ProjectGoal: string;
  ProjectObjective: string;
  MainActivities: string;
  DonorName: string;
  DonorContactPerson: string;
  DonorContactDesignation: string;
  DonorContactPersonEmail: string;
  DonorContactPersonCellId: string;
  REOIReceive: string;
  SubmissionDate: string;
  StrengthConsideration: string;
  GenderConsideration: string;
  RemarksGender: string;
  Security: string;
  SecurityConsideration: string;
  RemarksSecurity: string;
  Comments: string;
}

export class PMUProjectList {
  ID: number;
  ProjectName: string;
}

const projectsPmu: PMUProjectList[] = [
  {
    ID: 1,
    ProjectName: 'Welfare'
  },
  {
    ID: 2,
    ProjectName: 'Xenon'
  },
  {
    ID: 3,
    ProjectName: 'Amada'
  }
];

@Injectable()
export class PmuService {
  getPMUProjects() {
    return pmuprojects;
  }

  getPMUProjectsList() {
    return projectsPmu;
  }
}
