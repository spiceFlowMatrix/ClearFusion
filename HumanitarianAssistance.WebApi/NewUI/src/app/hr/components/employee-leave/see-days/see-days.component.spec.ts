import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SeeDaysComponent } from './see-days.component';

describe('SeeDaysComponent', () => {
  let component: SeeDaysComponent;
  let fixture: ComponentFixture<SeeDaysComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SeeDaysComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SeeDaysComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
