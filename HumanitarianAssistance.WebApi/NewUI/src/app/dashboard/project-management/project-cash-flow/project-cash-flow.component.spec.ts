import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectCashFlowComponent } from './project-cash-flow.component';

describe('ProjectCashFlowComponent', () => {
  let component: ProjectCashFlowComponent;
  let fixture: ComponentFixture<ProjectCashFlowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectCashFlowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectCashFlowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
