import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewAdvanceRequestComponent } from './new-advance-request.component';

describe('NewAdvanceRequestComponent', () => {
  let component: NewAdvanceRequestComponent;
  let fixture: ComponentFixture<NewAdvanceRequestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewAdvanceRequestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewAdvanceRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
