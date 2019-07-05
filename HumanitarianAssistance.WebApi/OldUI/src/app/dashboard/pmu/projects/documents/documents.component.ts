import { Component, OnInit } from '@angular/core';
import { Documents, ProjectsService } from '../projects.service';

@Component({
  selector: 'app-documents',
  templateUrl: './documents.component.html',
  styleUrls: ['./documents.component.css']
})
export class DocumentsComponent {

  docs: Documents[];
  dataSource: any;
  // data: any;
  // question: string[];
  // showFilterRow: boolean;
  tab1: any;

  constructor(private projectsService: ProjectsService) { 
    //TODO: Edit popup dropdown
    this.docs = this.projectsService.getDocs();
    this.dataSource = {
      store: {
        type: 'array',
        key: 'ID',
        data: this.projectsService.getDocs()
      }
    }
  } 
  
  doctype = [
    { DocTypeId: 1, DocTypeName: 'Lessons Learnt'},
    { DocTypeId: 2, DocTypeName: 'Success Stories'}
  ];
}
