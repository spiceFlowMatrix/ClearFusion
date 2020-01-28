import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppraisalConfigComponent } from './appraisal-config.component';

describe('AppraisalConfigComponent', () => {
  let component: AppraisalConfigComponent;
  let fixture: ComponentFixture<AppraisalConfigComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppraisalConfigComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppraisalConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
