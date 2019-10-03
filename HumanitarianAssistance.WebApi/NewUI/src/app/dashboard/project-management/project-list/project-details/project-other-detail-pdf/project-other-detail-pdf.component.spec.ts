import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectOtherDetailPdfComponent } from './project-other-detail-pdf.component';

describe('ProjectOtherDetailPdfComponent', () => {
  let component: ProjectOtherDetailPdfComponent;
  let fixture: ComponentFixture<ProjectOtherDetailPdfComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectOtherDetailPdfComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectOtherDetailPdfComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
