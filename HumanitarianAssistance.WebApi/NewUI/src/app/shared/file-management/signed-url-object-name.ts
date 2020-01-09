import { FileSourceEntityTypes } from '../enum';

 export class SignedUrlObjectName {
  static getSignedURLObjectName(pageId: number, entityId?: number): string {
    let objectName = '';

    switch (pageId) {
      case FileSourceEntityTypes.Voucher:
      objectName = 'voucher/';
      break;
      case FileSourceEntityTypes.StorePurchase:
      objectName = 'storepurchase/' + entityId + '/';
      break;
      case FileSourceEntityTypes.Employee:
      objectName = 'employeeprofileimage/' + entityId + '/';
      break;
      case FileSourceEntityTypes.ComparativeStatement:
      objectName = 'comparativeStatement/' + entityId + '/';
      break;
      case FileSourceEntityTypes.HiringRequestCandidateCV:
      objectName = 'HiringRequestCandidateCV/' + entityId + '/';
      break;
      case FileSourceEntityTypes.ProjectLogisticPurchase:
      objectName = 'ProjectLogisticPurchase/' + entityId + '/';
      break;
      case FileSourceEntityTypes.GoodsRecievedDocument:
      objectName = 'GoodsRecievedDocument/' + entityId + '/';
      break;
      case FileSourceEntityTypes.LogisticSupplierInvoice:
      objectName = 'LogisticSupplierInvoice/' + entityId + '/';
      break;
      case FileSourceEntityTypes.LogisticSupplierWarranty:
      objectName = 'LogisticSupplierWarranty/' + entityId + '/';
      break;
      case FileSourceEntityTypes.TenderProposalDocument:
      objectName = 'TenderProposalDocument/' + entityId + '/';
      break;
      case FileSourceEntityTypes.TenderRFPDocument:
      objectName = 'TenderRFPDocument/' + entityId + '/';
      break;
      case FileSourceEntityTypes.TenderAnnouncementDocument:
      objectName = 'TenderAnnouncementDocument/' + entityId + '/';
      break;
      case FileSourceEntityTypes.TenderBidContractLetter:
      objectName = 'TenderBidContractLetter/' + entityId + '/';
      break;
    }

    return objectName;
  }
 }
