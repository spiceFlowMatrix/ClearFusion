import { Component} from '@angular/core';
import { Beneficiaries1, ProjectsService } from '../projects.service';

@Component({
  selector: 'app-beneficiaries',
  templateUrl: './beneficiaries.component.html',
  styleUrls: ['./beneficiaries.component.css']
})
export class BeneficiariesComponent {

  beneficiaries: Beneficiaries1[];

  
  dataSource: any;
  // data: any;
  // question: string[];
  // showFilterRow: boolean;
  tab1: any;
  date: Date = new Date()

  constructor(private projectsService: ProjectsService) { 
    //TODO: Edit popup dropdown
    this.beneficiaries = this.projectsService.getBeneficiaries();
    this.dataSource = {
      store: {
        type: 'array',
        key: 'ID',
        data: this.projectsService.getBeneficiaries()
      }


  }

} 

    sextypes = [
      { SexTypeId: 1, SexTypeName: 'Male'},
      { SexTypeId: 2, SexTypeName: 'Female'}
    ];

    maritalStatustypes = [
      { maritalStatusId: 1, maritalStatusName: 'Single'},
      { maritalStatusId: 2, maritalStatusName: 'Married'},
      { maritalStatusId: 3, maritalStatusName: 'Widow'},
      { maritalStatusId: 4, maritalStatusName: 'Separated'},
      { maritalStatusId: 5, maritalStatusName: 'Divorced'}
    ];
}
