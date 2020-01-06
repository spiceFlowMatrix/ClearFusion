import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TenderBidSelectionComponent } from './tender-bid-selection.component';

describe('TenderBidSelectionComponent', () => {
  let component: TenderBidSelectionComponent;
  let fixture: ComponentFixture<TenderBidSelectionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TenderBidSelectionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TenderBidSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
