export const topics = {
  fixtureChanged: 'offer.fixture-changed.v1',
  priceChanged: 'offer.price-changed.v1',
  resultPublished: 'offer.result-published.v1',
  couponPlaced: 'placement.coupon-placed.v1',
  couponRejected: 'placement.coupon-rejected.v1',
  ledgerPosted: 'wallet.ledger-posted.v1',
  legEvaluated: 'settlement.leg-evaluated.v1',
  couponSettled: 'settlement.coupon-settled.v1',
  stuckCoupon: 'settlement.stuck-coupon.v1',
  payoutDeadLetter: 'payout.dead-letter.v1',
  payoutCompleted: 'payout.payout-completed.v1',
  incidentRaised: 'steward.incident-raised.v1',
  incidentUpdated: 'steward.incident-updated.v1',
  remediationExecuted: 'steward.remediation-executed.v1',
  liabilityChanged: 'risk.liability-changed.v1',
  riskAlert: 'risk.risk-alert.v1',
} as const;

export type TopicBase = (typeof topics)[keyof typeof topics];
