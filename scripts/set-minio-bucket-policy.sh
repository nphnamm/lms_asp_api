#!/bin/bash

# Script to set MinIO bucket policy for public read access
# This is a backup method if the automatic policy setting doesn't work

echo "Setting MinIO bucket policy for public read access..."

# MinIO configuration
MINIO_ENDPOINT="localhost:9000"
BUCKET_NAME="lms-media"
ACCESS_KEY="minioadmin"
SECRET_KEY="minioadmin"

# Create the policy JSON
POLICY_JSON='{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Principal": {
        "AWS": "*"
      },
      "Action": "s3:GetObject",
      "Resource": "arn:aws:s3:::'$BUCKET_NAME'/*"
    },
    {
      "Effect": "Allow",
      "Principal": "*",
      "Action": "s3:GetObject",
      "Resource": "arn:aws:s3:::'$BUCKET_NAME'/*"
    }
  ]
}'

# Set the bucket policy using MinIO client
mc alias set myminio http://$MINIO_ENDPOINT $ACCESS_KEY $SECRET_KEY
mc policy set-json <(echo "$POLICY_JSON") myminio/$BUCKET_NAME

echo "Bucket policy set successfully!"
echo "You can now access files directly at: http://$MINIO_ENDPOINT/$BUCKET_NAME/{filename}"
