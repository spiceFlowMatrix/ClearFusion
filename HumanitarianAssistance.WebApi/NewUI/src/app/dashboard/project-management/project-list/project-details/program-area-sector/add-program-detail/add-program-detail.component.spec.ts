import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddProgramDetailComponent } from './add-program-detail.component';

describe('AddProgramDetailComponent', () => {
  let component: AddProgramDetailComponent;
  let fixture: ComponentFixture<AddProgramDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddProgramDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddProgramDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
