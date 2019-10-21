import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddProjectIndicatorComponent } from './add-project-indicator.component';

describe('AddProjectIndicatorComponent', () => {
  let component: AddProjectIndicatorComponent;
  let fixture: ComponentFixture<AddProjectIndicatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddProjectIndicatorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddProjectIndicatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
