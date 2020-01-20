import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddHistoricalLogComponent } from './add-historical-log.component';

describe('AddHistoricalLogComponent', () => {
  let component: AddHistoricalLogComponent;
  let fixture: ComponentFixture<AddHistoricalLogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddHistoricalLogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddHistoricalLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
