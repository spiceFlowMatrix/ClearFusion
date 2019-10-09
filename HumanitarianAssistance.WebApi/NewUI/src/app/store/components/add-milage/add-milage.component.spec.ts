import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMilageComponent } from './add-milage.component';

describe('AddMilageComponent', () => {
  let component: AddMilageComponent;
  let fixture: ComponentFixture<AddMilageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddMilageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddMilageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
