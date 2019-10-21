import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MediumDetailsComponent } from './medium-details.component';

describe('MediumDetailsComponent', () => {
  let component: MediumDetailsComponent;
  let fixture: ComponentFixture<MediumDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MediumDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MediumDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
