import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PeopleAddFormComponent } from './people-add-form.component';

describe('PeopleAddFormComponent', () => {
  let component: PeopleAddFormComponent;
  let fixture: ComponentFixture<PeopleAddFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PeopleAddFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PeopleAddFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
